using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using System.Drawing;
using th_poker_api.Model.Amount;
using th_poker_api.Model.Card;
using th_poker_api.Model.Friend;
using th_poker_api.Model.Player;
using th_poker_api.Model.Purchase;
using th_poker_api.Model.Room;
using th_poker_api.Model.WebDashboard.Misc;
using th_poker_api.Model.WebDashboard.Purchase;
using th_poker_api.Model.WebDashboard.User;
using th_poker_api.Model.ZTesting;

namespace th_poker_api.Data
{
    public class DataContext : DbContext
    {

        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder
                .UseSqlServer("Server=tcp:perpusku.database.windows.net,1433;Initial Catalog=db-poker;Persist Security Info=False;User ID=perpusadmin;Password=Nander81;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MDFriendship>(entity =>
            {
                entity.ToTable("MDFriendship");

                entity.HasKey(key => new { key.RequesterId, key.AddresseeId });
                
                //Addressee
                entity.HasOne(f => f.Addressee)
                    .WithMany(u => u.FriendshipAddressee)
                    .HasForeignKey(f => f.AddresseeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FriendshipToAddressee_FK");

                //Requester
                entity.HasOne(f => f.Requester)
                    .WithMany(u => u.FriendshipRequester)
                    .HasForeignKey(f => f.RequesterId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FriendshipToRequester_FK");
            });

            modelBuilder.Entity<MDPaymentItem>().HasData(
               new MDPaymentItem
               {
                   IdPymItem = Guid.NewGuid().ToString(),
                   Value = 40000000000,
                   Price = 67000,
                   Desc = "40B",
                   CreatedBy = "System",
                   CreatedOn = DateTime.Now,
               },

               new MDPaymentItem
               {
                   IdPymItem = Guid.NewGuid().ToString(),
                   Value = 250000000000,
                   Price = 259000,
                   Desc = "250B",
                   CreatedBy = "System",
                   CreatedOn = DateTime.Now,
               },

               new MDPaymentItem
               {
                   IdPymItem = Guid.NewGuid().ToString(),
                   Value = 750000000000,
                   Price = 519000,
                   Desc = "750B",
                   CreatedBy = "System",
                   CreatedOn = DateTime.Now,
               },

               new MDPaymentItem
               {
                   IdPymItem = Guid.NewGuid().ToString(),
                   Value = 1000000000000,
                   Price = 719000,
                   Desc = "1T",
                   CreatedBy = "System",
                   CreatedOn = DateTime.Now,
               },

               new MDPaymentItem
               {
                   IdPymItem = Guid.NewGuid().ToString(),
                   Value = 3000000000000,
                   Price = 1099000,
                   Desc = "3T",
                   CreatedBy = "System",
                   CreatedOn = DateTime.Now,
               },

               new MDPaymentItem
               {
                   IdPymItem = Guid.NewGuid().ToString(),
                   Value = 6000000000000,
                   Price = 1590000,
                   Desc = "6T",
                   CreatedBy = "System",
                   CreatedOn = DateTime.Now,
               }


               );
            /*
                        #################################################
                         Created By : William Tan
                         Created On : 2/23/2023
                         Script     : DataContext
                         Desc       : Create Data Seeding in Data Context
                        #################################################

                        Create Seeding Data WinType
                        #region Wintype
                        Adding Database By Seeding for WinType
                        modelBuilder.Entity<WinType>().HasData(

                            new WinType
                            {
                                IdWinType = 1,
                                Desc = "High Card",
                                CreatedBy = "System",
                                CreatedOn = DateTime.Now
                            },
                            new WinType
                            {
                                IdWinType = 2,
                                Desc = "Pair",
                                CreatedBy = "System",
                                CreatedOn = DateTime.Now
                            },
                            new WinType
                            {
                                IdWinType = 3,
                                Desc = "Two pair",
                                CreatedBy = "System",
                                CreatedOn = DateTime.Now
                            },
                            new WinType
                            {
                                IdWinType = 4,
                                Desc = "Three of a kind",
                                CreatedBy = "System",
                                CreatedOn = DateTime.Now
                            },
                            new WinType
                            {
                                IdWinType = 5,
                                Desc = "Straight",
                                CreatedBy = "System",
                                CreatedOn = DateTime.Now
                            },
                            new WinType
                            {
                                IdWinType = 6,
                                Desc = "Flush",
                                CreatedBy = "System",
                                CreatedOn = DateTime.Now
                            },
                            new WinType
                            {
                                IdWinType = 7,
                                Desc = "Full House",
                                CreatedBy = "System",
                                CreatedOn = DateTime.Now
                            },
                            new WinType
                            {
                                IdWinType = 8,
                                Desc = "Four of a Kind",
                                CreatedBy = "System",
                                CreatedOn = DateTime.Now
                            },
                            new WinType
                            {
                                IdWinType = 9,
                                Desc = "Straight Flush",
                                CreatedBy = "System",
                                CreatedOn = DateTime.Now
                            },
                            new WinType
                            {
                                IdWinType = 10,
                                Desc = "Royal Flush",
                                CreatedBy = "System",
                                CreatedOn = DateTime.Now
                            });
                        #endregion

                        Create Seeding Data MDRoles
                        #region MDRoles
                        modelBuilder.Entity<MDRoles>().HasData(

                            new MDRoles
                            {
                                Id = 1,
                                Desc = "Player",
                                CreatedBy = "SYSTEM",
                                CreatedOn = DateTime.Now,
                                UpdatedBy = "SYSTEM",
                                UpdatedOn = DateTime.Now
                            },
                            new MDRoles
                            {
                                Id = 2,
                                Desc = "Admin",
                                CreatedBy = "SYSTEM",
                                CreatedOn = DateTime.Now,
                                UpdatedBy = "SYSTEM",
                                UpdatedOn = DateTime.Now
                            },
                            new MDRoles
                            {
                                Id = 3,
                                Desc = "User",
                                CreatedBy = "SYSTEM",
                                CreatedOn = DateTime.Now,
                                UpdatedBy = "SYSTEM",
                                UpdatedOn = DateTime.Now
                            });
                        #endregion

                        Create Seeding Data MDStatuses
                        #region MdStatuses
                        modelBuilder.Entity<MDStatuses>().HasData(

                            new MDStatuses
                            {
                                IdStatus= 1,
                                Desc = "Active",
                                CreatedBy = "SYSTEM",
                                CreatedOn = DateTime.Now,
                                UpdatedBy = "SYSTEM",
                                UpdatedOn = DateTime.Now
                            },
                            new MDStatuses
                            {
                                IdStatus = 2,
                                Desc = "Banned",
                                CreatedBy = "SYSTEM",
                                CreatedOn = DateTime.Now,
                                UpdatedBy = "SYSTEM",
                                UpdatedOn = DateTime.Now
                            },
                            new MDStatuses
                            {
                                IdStatus = 3,
                                Desc = "PAID",
                                CreatedBy = "SYSTEM",
                                CreatedOn = DateTime.Now,
                                UpdatedBy = "SYSTEM",
                                UpdatedOn = DateTime.Now
                            },
                            new MDStatuses
                            {
                                IdStatus = 4,
                                Desc = "UNPAID",
                                CreatedBy = "SYSTEM",
                                CreatedOn = DateTime.Now,
                                UpdatedBy = "SYSTEM",
                                UpdatedOn = DateTime.Now
                            },
                            new MDStatuses
                            {
                                IdStatus = 5,
                                Desc = "HOLD",
                                CreatedBy = "SYSTEM",
                                CreatedOn = DateTime.Now,
                                UpdatedBy = "SYSTEM",
                                UpdatedOn = DateTime.Now
                            });
                        #endregion

                        Create Seeding Data MDGameTypes
                        #region MDGameTypes
                        modelBuilder.Entity<MDGameTypes>().HasData(

                            new MDGameTypes
                            {
                                IdMDGameType = 1.ToString(),
                                GameDesc = "Poker",
                                UpdatedBy = "SYSTEM",
                                UpdatedOn = DateTime.Now,
                            });
                        #endregion

                        Create Seeding Data MDGames
                        #region MDGames
                        modelBuilder.Entity<MDGames>().HasData(

                            new MDGames
                            {        
                                GameDesc = "Casino \n Singapore",
                                GameTitle = "Marina Bay sands",
                                IdMDGames = Guid.NewGuid().ToString(),
                                BuyInMin = 2000,
                                BuyInMax = 10000,
                                StakesMin = 500,
                                StakesMax = 1000,
                                isActive = true,
                                UpdatedBy = "System",
                                UpdatedOn = DateTime.Now,
                                CreatedBy= "System",
                                CreatedOn= DateTime.Now,
                           },
                           new MDGames
                           {
                               GameDesc = "Casino \n Lisboa",
                               GameTitle = "",
                               IdMDGames = Guid.NewGuid().ToString(),
                               BuyInMin = 5000,
                               BuyInMax = 20000,
                               StakesMin = 800,
                               StakesMax = 1500,
                               isActive = true,
                               UpdatedBy = "System",
                               UpdatedOn = DateTime.Now,
                               CreatedBy = "System",
                               CreatedOn = DateTime.Now,
                           },
                           new MDGames
                           {
                               GameDesc = "Las Vegas \n Nevada",
                               GameTitle = "",
                               IdMDGames = Guid.NewGuid().ToString(),
                               BuyInMin = 10000,
                               BuyInMax = 30000,
                               StakesMin = 1500,
                               StakesMax = 3000,
                               isActive = true,
                               UpdatedBy = "System",
                               UpdatedOn = DateTime.Now,
                               CreatedBy = "System",
                               CreatedOn = DateTime.Now,
                           });
                        modelBuilder.Entity<MDCards>().HasData(
                            new MDCards 
                            {
                                Id = 1,
                                IndexCard = 0,
                                Desc = "Clover 2",
                                CreatedBy = "SYSTEM",
                                CreatedOn = DateTime.Now,
                                UpdatedBy = "SYSTEM",
                                UpdatedOn = DateTime.Now,
                            },
                            new MDCards
                            {
                                Id = 2,
                                IndexCard = 1,
                                Desc = "Diamond 2",
                                CreatedBy = "SYSTEM",
                                CreatedOn = DateTime.Now,
                                UpdatedBy = "SYSTEM",
                                UpdatedOn = DateTime.Now,
                            },
                            new MDCards
                            {
                                Id = 3,
                                IndexCard = 2,
                                Desc = "Heart 2",
                                CreatedBy = "SYSTEM",
                                CreatedOn = DateTime.Now,
                                UpdatedBy = "SYSTEM",
                                UpdatedOn = DateTime.Now,
                            },
                            new MDCards
                            {
                                Id = 4,
                                IndexCard = 3,
                                Desc = "Spade 2",
                                CreatedBy = "SYSTEM",
                                CreatedOn = DateTime.Now,
                                UpdatedBy = "SYSTEM",
                                UpdatedOn = DateTime.Now,
                            },
                            new MDCards
                            {
                                Id = 5,
                                IndexCard = 4,
                                Desc = "Clover 3",
                                CreatedBy = "SYSTEM",
                                CreatedOn = DateTime.Now,
                                UpdatedBy = "SYSTEM",
                                UpdatedOn = DateTime.Now,
                            },
                            new MDCards
                            {
                                Id = 6,
                                IndexCard = 5,
                                Desc = "Diamond 3",
                                CreatedBy = "SYSTEM",
                                CreatedOn = DateTime.Now,
                                UpdatedBy = "SYSTEM",
                                UpdatedOn = DateTime.Now,
                            },
                            new MDCards
                            {   Id = 7,
                                IndexCard = 6,
                                Desc = "Heart 3",
                                CreatedBy = "SYSTEM",
                                CreatedOn = DateTime.Now,
                                UpdatedBy = "SYSTEM",
                                UpdatedOn = DateTime.Now,
                            },
                            new MDCards
                            {   Id = 8,
                                IndexCard = 7,
                                Desc = "Spade 3",
                                CreatedBy = "SYSTEM",
                                CreatedOn = DateTime.Now,
                                UpdatedBy = "SYSTEM",
                                UpdatedOn = DateTime.Now,
                            },
                            new MDCards
                            {   Id = 9,
                                IndexCard = 8,
                                Desc = "Clover 4",
                                CreatedBy = "SYSTEM",
                                CreatedOn = DateTime.Now,
                                UpdatedBy = "SYSTEM",
                                UpdatedOn = DateTime.Now,
                            },
                            new MDCards
                            {   Id = 10,
                                IndexCard = 9,
                                Desc = "Diamond 4",
                                CreatedBy = "SYSTEM",
                                CreatedOn = DateTime.Now,
                                UpdatedBy = "SYSTEM",
                                UpdatedOn = DateTime.Now,
                            },
                            new MDCards
                            {   Id = 11,
                                IndexCard = 10,
                                Desc = "Heart 4",
                                CreatedBy = "SYSTEM",
                                CreatedOn = DateTime.Now,
                                UpdatedBy = "SYSTEM",
                                UpdatedOn = DateTime.Now,
                            },
                            new MDCards
                            {   Id = 12,
                                IndexCard = 11,
                                Desc = "Spade 4",
                                CreatedBy = "SYSTEM",
                                CreatedOn = DateTime.Now,
                                UpdatedBy = "SYSTEM",
                                UpdatedOn = DateTime.Now,
                            },
                            new MDCards
                            {   Id = 13,
                                IndexCard = 12,
                                Desc = "Clover 5",
                                CreatedBy = "SYSTEM",
                                CreatedOn = DateTime.Now,
                                UpdatedBy = "SYSTEM",
                                UpdatedOn = DateTime.Now,
                            },
                            new MDCards
                            {   Id = 14,
                                IndexCard = 13,
                                Desc = "Diamond 5",
                                CreatedBy = "SYSTEM",
                                CreatedOn = DateTime.Now,
                                UpdatedBy = "SYSTEM",
                                UpdatedOn = DateTime.Now,
                            },
                            new MDCards
                            {   Id = 15,
                                IndexCard = 14,
                                Desc = "Heart 5",
                                CreatedBy = "SYSTEM",
                                CreatedOn = DateTime.Now,
                                UpdatedBy = "SYSTEM",
                                UpdatedOn = DateTime.Now,
                            },
                            new MDCards
                            {   Id = 16,
                                IndexCard = 15,
                                Desc = "Spade 5",
                                CreatedBy = "SYSTEM",
                                CreatedOn = DateTime.Now,
                                UpdatedBy = "SYSTEM",
                                UpdatedOn = DateTime.Now,
                            },
                            new MDCards
                            {   Id = 17,
                                IndexCard = 16,
                                Desc = "Clover 6",
                                CreatedBy = "SYSTEM",
                                CreatedOn = DateTime.Now,
                                UpdatedBy = "SYSTEM",
                                UpdatedOn = DateTime.Now,
                            },
                            new MDCards
                            {   Id = 18,
                                IndexCard = 17,
                                Desc = "Diamond 6",
                                CreatedBy = "SYSTEM",
                                CreatedOn = DateTime.Now,
                                UpdatedBy = "SYSTEM",
                                UpdatedOn = DateTime.Now,
                            },
                            new MDCards
                            {   Id = 19,
                                IndexCard = 18,
                                Desc = "Heart 6",
                                CreatedBy = "SYSTEM",
                                CreatedOn = DateTime.Now,
                                UpdatedBy = "SYSTEM",
                                UpdatedOn = DateTime.Now,
                            },
                            new MDCards
                            {   Id = 20,
                                IndexCard = 19,
                                Desc = "Spade 6",
                                CreatedBy = "SYSTEM",
                                CreatedOn = DateTime.Now,
                                UpdatedBy = "SYSTEM",
                                UpdatedOn = DateTime.Now,
                            },
                            new MDCards
                            {   Id = 21,
                                IndexCard = 20,
                                Desc = "Clover 7",
                                CreatedBy = "SYSTEM",
                                CreatedOn = DateTime.Now,
                                UpdatedBy = "SYSTEM",
                                UpdatedOn = DateTime.Now,
                            },
                            new MDCards
                            {   Id = 22,      
                                IndexCard = 21,
                                Desc = "Diamond 7",
                                CreatedBy = "SYSTEM",
                                CreatedOn = DateTime.Now,
                                UpdatedBy = "SYSTEM",
                                UpdatedOn = DateTime.Now,
                            },
                            new MDCards
                            {   Id= 23,
                                IndexCard = 22,
                                Desc = "Heart 7",
                                CreatedBy = "SYSTEM",
                                CreatedOn = DateTime.Now,
                                UpdatedBy = "SYSTEM",
                                UpdatedOn = DateTime.Now,
                            },
                            new MDCards
                            {   Id = 24,
                                IndexCard = 23,
                                Desc = "Spade 7",
                                CreatedBy = "SYSTEM",
                                CreatedOn = DateTime.Now,
                                UpdatedBy = "SYSTEM",
                                UpdatedOn = DateTime.Now,
                            },
                            new MDCards
                            {
                                Id = 25,
                                IndexCard = 24,
                                Desc = "Clover 8",
                                CreatedBy = "SYSTEM",
                                CreatedOn = DateTime.Now,
                                UpdatedBy = "SYSTEM",
                                UpdatedOn = DateTime.Now,
                            },
                            new MDCards
                            {
                                Id = 26,
                                IndexCard = 25,
                                Desc = "Diamond 8",
                                CreatedBy = "SYSTEM",
                                CreatedOn = DateTime.Now,
                                UpdatedBy = "SYSTEM",
                                UpdatedOn = DateTime.Now,
                            },
                            new MDCards
                            {
                                Id = 27,
                                IndexCard = 26,
                                Desc = "Heart 8",
                                CreatedBy = "SYSTEM",
                                CreatedOn = DateTime.Now,
                                UpdatedBy = "SYSTEM",
                                UpdatedOn = DateTime.Now,
                            },
                            new MDCards
                            {
                                Id = 28,
                                IndexCard = 27,
                                Desc = "Spade 8",
                                CreatedBy = "SYSTEM",
                                CreatedOn = DateTime.Now,
                                UpdatedBy = "SYSTEM",
                                UpdatedOn = DateTime.Now,
                            },
                            new MDCards
                            {
                                Id = 29,
                                IndexCard = 28,
                                Desc = "Clover 9",
                                CreatedBy = "SYSTEM",
                                CreatedOn = DateTime.Now,
                                UpdatedBy = "SYSTEM",
                                UpdatedOn = DateTime.Now,
                            },
                            new MDCards
                            {
                                Id = 30,
                                IndexCard = 29,
                                Desc = "Diamond 9",
                                CreatedBy = "SYSTEM",
                                CreatedOn = DateTime.Now,
                                UpdatedBy = "SYSTEM",
                                UpdatedOn = DateTime.Now,
                            },
                            new MDCards
                            {
                                Id = 31,
                                IndexCard = 30,
                                Desc = "Heart 9",
                                CreatedBy = "SYSTEM",
                                CreatedOn = DateTime.Now,
                                UpdatedBy = "SYSTEM",
                                UpdatedOn = DateTime.Now,
                            },
                            new MDCards
                            {
                                Id = 32,
                                IndexCard = 31,
                                Desc = "Spade 9",
                                CreatedBy = "SYSTEM",
                                CreatedOn = DateTime.Now,
                                UpdatedBy = "SYSTEM",
                                UpdatedOn = DateTime.Now,
                            },
                            new MDCards
                            {
                                Id = 33,
                                IndexCard = 32,
                                Desc = "Clover 10",
                                CreatedBy = "SYSTEM",
                                CreatedOn = DateTime.Now,
                                UpdatedBy = "SYSTEM",
                                UpdatedOn = DateTime.Now,
                            },
                            new MDCards
                            {
                                Id = 34,
                                IndexCard = 33,
                                Desc = "Diamond 10",
                                CreatedBy = "SYSTEM",
                                CreatedOn = DateTime.Now,
                                UpdatedBy = "SYSTEM",
                                UpdatedOn = DateTime.Now,
                            },
                            new MDCards
                            {
                                Id = 35,
                                IndexCard = 34,
                                Desc = "Heart 10",
                                CreatedBy = "SYSTEM",
                                CreatedOn = DateTime.Now,
                                UpdatedBy = "SYSTEM",
                                UpdatedOn = DateTime.Now,
                            },
                            new MDCards
                            {
                                Id = 36,
                                IndexCard = 35,
                                Desc = "Spade 10",
                                CreatedBy = "SYSTEM",
                                CreatedOn = DateTime.Now,
                                UpdatedBy = "SYSTEM",
                                UpdatedOn = DateTime.Now,
                            },
                            new MDCards
                            {
                                Id = 37,
                                IndexCard = 36,
                                Desc = "Clover A",
                                CreatedBy = "SYSTEM",
                                CreatedOn = DateTime.Now,
                                UpdatedBy = "SYSTEM",
                                UpdatedOn = DateTime.Now,
                            },
                            new MDCards
                            {
                                Id = 38,
                                IndexCard = 37,
                                Desc = "Diamond A",
                                CreatedBy = "SYSTEM",
                                CreatedOn = DateTime.Now,
                                UpdatedBy = "SYSTEM",
                                UpdatedOn = DateTime.Now,
                            },
                            new MDCards
                            {
                                Id = 39,
                                IndexCard = 38,
                                Desc = "Heart A",
                                CreatedBy = "SYSTEM",
                                CreatedOn = DateTime.Now,
                                UpdatedBy = "SYSTEM",
                                UpdatedOn = DateTime.Now,
                            },
                            new MDCards
                            {
                                Id = 40,
                                IndexCard = 39,
                                Desc = "Spade A",
                                CreatedBy = "SYSTEM",
                                CreatedOn = DateTime.Now,
                                UpdatedBy = "SYSTEM",
                                UpdatedOn = DateTime.Now,
                            },
                            new MDCards
                            {
                                Id = 41,
                                IndexCard = 40,
                                Desc = "Clover J",
                                CreatedBy = "SYSTEM",
                                CreatedOn = DateTime.Now,
                                UpdatedBy = "SYSTEM",
                                UpdatedOn = DateTime.Now,
                            },
                            new MDCards
                            {
                                Id = 42,
                                IndexCard = 41,
                                Desc = "Diamond J",
                                CreatedBy = "SYSTEM",
                                CreatedOn = DateTime.Now,
                                UpdatedBy = "SYSTEM",
                                UpdatedOn = DateTime.Now,
                            },
                            new MDCards
                            {
                                Id = 43,
                                IndexCard = 42,
                                Desc = "Heart J",
                                CreatedBy = "SYSTEM",
                                CreatedOn = DateTime.Now,
                                UpdatedBy = "SYSTEM",
                                UpdatedOn = DateTime.Now,
                            },
                            new MDCards
                            {
                                Id = 44,
                                IndexCard = 43,
                                Desc = "Spade J",
                                CreatedBy = "SYSTEM",
                                CreatedOn = DateTime.Now,
                                UpdatedBy = "SYSTEM",
                                UpdatedOn = DateTime.Now,
                            },
                            new MDCards
                            {
                                Id = 45,
                                IndexCard = 44,
                                Desc = "Clover Q",
                                CreatedBy = "SYSTEM",
                                CreatedOn = DateTime.Now,
                                UpdatedBy = "SYSTEM",
                                UpdatedOn = DateTime.Now,
                            },
                            new MDCards
                            {
                                Id = 46,
                                IndexCard = 45,
                                Desc = "Diamond Q",
                                CreatedBy = "SYSTEM",
                                CreatedOn = DateTime.Now,
                                UpdatedBy = "SYSTEM",
                                UpdatedOn = DateTime.Now,
                            },
                            new MDCards
                            {
                                Id = 47,
                                IndexCard = 46,
                                Desc = "Heart Q",
                                CreatedBy = "SYSTEM",
                                CreatedOn = DateTime.Now,
                                UpdatedBy = "SYSTEM",
                                UpdatedOn = DateTime.Now,
                            },
                            new MDCards
                            {
                                Id = 48,
                                IndexCard = 47,
                                Desc = "Spade Q",
                                CreatedBy = "SYSTEM",
                                CreatedOn = DateTime.Now,
                                UpdatedBy = "SYSTEM",
                                UpdatedOn = DateTime.Now,
                            },
                            new MDCards
                            {
                                Id = 49,
                                IndexCard = 48,
                                Desc = "Clover K",
                                CreatedBy = "SYSTEM",
                                CreatedOn = DateTime.Now,
                                UpdatedBy = "SYSTEM",
                                UpdatedOn = DateTime.Now,
                            },
                            new MDCards
                            {
                                Id = 50,
                                IndexCard = 49,
                                Desc = "Diamond K",
                                CreatedBy = "SYSTEM",
                                CreatedOn = DateTime.Now,
                                UpdatedBy = "SYSTEM",
                                UpdatedOn = DateTime.Now,
                            },
                            new MDCards
                            {
                                Id = 51,
                                IndexCard = 50,
                                Desc = "Heart K",
                                CreatedBy = "SYSTEM",
                                CreatedOn = DateTime.Now,
                                UpdatedBy = "SYSTEM",
                                UpdatedOn = DateTime.Now,
                            },
                            new MDCards
                            {
                                Id = 52,
                                IndexCard = 51,
                                Desc = "Spade K",
                                CreatedBy = "SYSTEM",
                                CreatedOn = DateTime.Now,
                                UpdatedBy = "SYSTEM",
                                UpdatedOn = DateTime.Now,
                            } );
                        modelBuilder.Entity<MDPaymentItem>().HasData(
                            new MDPaymentItem
                            {
                                IdPymItem = Guid.NewGuid().ToString(),
                                Value = 10,
                                Price = 50,
                                Desc = "Bronze",
                                CreatedBy = "System",
                                CreatedOn = DateTime.Now,
                            },
                            new MDPaymentItem
                            {
                                IdPymItem = Guid.NewGuid().ToString(),
                                Value = 20,
                                Price = 100,
                                Desc = "Silver",
                                CreatedBy = "System",
                                CreatedOn = DateTime.Now,
                            },
                            new MDPaymentItem
                            {
                                IdPymItem = Guid.NewGuid().ToString(),
                                Value = 30,
                                Price = 150,
                                Desc = "Gold",
                                CreatedBy = "System",
                                CreatedOn = DateTime.Now,
                            },
                            new MDPaymentItem
                            {
                                IdPymItem = Guid.NewGuid().ToString(),
                                Value = 40,
                                Price = 200,
                                Desc = "Diamond",
                                CreatedBy = "System",
                                CreatedOn = DateTime.Now,
                            },
                            new MDPaymentItem
                            {
                                IdPymItem = Guid.NewGuid().ToString(),
                                Value = 50,
                                Price = 250,
                                Desc = "Platinum",
                                CreatedBy = "System",
                                CreatedOn = DateTime.Now,
                            },
                            new MDPaymentItem
                            {
                                IdPymItem = Guid.NewGuid().ToString(),
                                Value = 60,
                                Price = 300,
                                Desc = "Legend",
                                CreatedBy = "System",
                                CreatedOn = DateTime.Now,
                            },
                            new MDPaymentItem
                            {
                                IdPymItem = Guid.NewGuid().ToString(),
                                Value = 100,
                                Price = 500,
                                Desc = "Mythic",
                                CreatedBy = "System",
                                CreatedOn = DateTime.Now,
                            });
                        #endregion
            */
            base.OnModelCreating(modelBuilder);
        }
    #region Database
        #region Modul Global
        public DbSet<MDStatuses> MDStatuses { get; set; }
        #endregion

        #region Modul Users
        public DbSet<MDUsers> MDUsers { get; set; }
        public DbSet<MDRoles> MDRoles { get; set; }
        public DbSet<UsersProfile> UsersProfile { get; set; }
        public DbSet<UsersToken> UsersToken { get; set; }
        public DbSet<UsersReferal> UsersReferal { get; set; }
        public DbSet<UsersLogin> UsersLogin { get; set; }
        #endregion

        #region Modul Room
        public DbSet<MDRoomList> MDRoomList { get; set; }
        #endregion

        #region Modul Cards
        public DbSet<MDCards> MDCards { get; set; }
        #endregion

        #region Modul Games
        public DbSet<MDGames> MDGames { get; set; }
        public DbSet<MDGameTypes> MDGameType { get; set; }
        public DbSet<GameplayHeader> GameplayHeader { get; set; }
        public DbSet<GameplayDetail> GameplayDetail { get; set; }
        public DbSet<WinType> WinType { get; set; }
        public DbSet<TSBigJackpot> TSBigJackpots { get; set; }
        public DbSet<TSJackpot> TSJackpots { get; set; }
        public DbSet<TSHouse> TSHouses { get; set; }
        public DbSet<TSJackpotWinner> TSJackpotWinners { get; set; }
        #endregion

        #region Modul Purchase
        public DbSet<MDPaymentItem> MDPaymentItem { get; set; }
        //public DbSet<MDPaymentMethod> MDPaymentMethod { get; set; }
        //public DbSet<MDPrice> MDPrice { get; set; }
        #endregion


        #region Modul Transactions
        public DbSet<TSPurchase> TSPurchases { get; set; }
        #endregion

        #region Modul Amount
        public DbSet<UsersAmount> UserAmount { get; set; }
        #endregion

        #region Modul Friend
        public DbSet<MDFriends> MDFriends { get; set; }
        public DbSet<MDFriendship> MDFriendship { get; set; }
        #endregion

        #region Testing
        public DbSet<Statuses> Statuses { get; set; }
        public DbSet<PaymentHistory> PaymentHistory { get; set; }
        public DbSet<PaymentMethod> paymentMethods { get; set; }
        public DbSet<RolesDashboard> RolesDashboard { get; set; }
        public DbSet<UserLoginDashboard> UserLoginDashboard { get; set; }
        public DbSet<TableBet> tableBets { get; set; }
        public DbSet<TSTransferChip> TSTransferChip { get; set; }
        #endregion
        #endregion
    }
}
