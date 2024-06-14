using AutoMapper;
using Microsoft.VisualBasic;
using System;
namespace th_poker_api.Common
{
    public class Functions
    {
        private DataContext _context = new DataContext();
        private APIKey keys = new APIKey();

        public string fileLoc = "http://10.10.10.115:81/file/Pictures/"; //warning: developer code is only use for seeding

        public string urlApi = "https://thehome.float-zone.com:80/";
        //public string urlApi = "http://api.thehome.biz.id:81/File/ProfilePicture/Male1.png";

        public float getAmountPurchase(string item)
        {
            float amount = 0;

            int items = Convert.ToInt32(item);

            switch (items)
            {
                case 1:
                    amount = 40000000000;
                    break;
                case 2:
                    amount = 250000000000;
                    break;
                case 3:
                    amount = 750000000000;
                    break;
                case 4:
                    amount = 1000000000000;
                    break;
                case 5:
                    amount = 3000000000000;
                    break;
                case 6:
                    amount = 6000000000000;
                    break;
            }

            return amount;
        }

        #region Amount Count Method
        public float getAmount(string userId)
        {
            MDUsers user = _context.MDUsers.Where(Q => Q.UserId.Equals(userId)).FirstOrDefault();

            //first step: hitung spinning wheel/freebies
            var spinning = _context.UserAmount.Where(u => u.IdUser.Equals(userId)).Sum(v => v.amount);

            //second step: hitung topup
            var topup = _context.TSPurchases.Where(u => u.IdUser.Equals(userId)).Sum(v => v.Amount);

            //3.1 : hitung jumlah menang, 1 = win, 2 = loss, 3 = fold
            var wins = _context.GameplayDetail.Where(u => u.IdUser.Equals(user) && u.Status == "0").Sum(p => p.Balance);

            //3.2 : hitung jumlah bet
            var loss = _context.tableBets.Where(u => u.createdBy.Equals(userId)).Sum(p => p.amt);

            //3.3 : hitung jumlah uang yang diterima
            var receive = _context.TSTransferChip.Where(x => x.Receiver.Equals(userId)).Sum(x => x.Amount);

            //3.4 : hitung jumlah uang yang dikirim
            var send = _context.TSTransferChip.Where(x => x.Sender.Equals(userId)).Sum(x => x.Amount);

            //3.5 : hitung jumlah jackpot menang nya

            var jp = _context.TSJackpotWinners.Where(x => x.CreatedAt.Equals(userId)).Sum(x => x.Amount);

            float amount = (float)((spinning + topup + wins + receive + jp) - (loss + send));

            return amount;
        }
        public float totalJP (string gameId)
        {

            double jp = _context.TSJackpots.Where(x =>x.GameID.Equals(gameId)).Sum(x => x.Amount);
            float amount = (float)jp;
            
            return amount;
        }

        public float totalBJP(string gameId)
        {
            double jp = _context.TSBigJackpots.Where(x => x.GameID.Equals(gameId)).Sum(x => x.Pool);
            float amount = (float)jp;
            return amount;
        }


  
        #endregion
        public Boolean validateAPIKey(string apiKey)
        {
            return keys.Key.Equals(apiKey);
        }
        public Boolean checkDeveloperCode(string devcode)
        {
            return keys.developerCode.Equals(devcode);
        }
        public bool validateString(string value)
        {
            return String.IsNullOrEmpty(value);
        }

        public int sumint(int[] data)
        {
            return data.Sum();
        }
        public float sumFloat(float[] data)
        {
            return data.Sum();
        }
        public double sumDouble(double[] data)
        {
            return data.Sum();
        }
        public double averageInt(int[] data)
        {
            return data.Average();
        }
        public int getMaxInt(int[] data)
        {
            return data.Max();
        }
        public float getMaxFloat(float[] data)
        {
            return data.Max();
        }
        public double getMaxDouble(double[] data)
        {
            return data.Max();
        }
        public int getMinInt(int[] data)
        {
            return data.Min();
        }
        public float getMinFloat(float[] data)
        {
            return data.Min();
        }
        public double getMinDouble(double[] data)
        {
            return data.Min();
        }
    }
}

