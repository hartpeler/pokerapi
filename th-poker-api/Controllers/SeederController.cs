using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using th_poker_api.DTO.MasterData;
using th_poker_api.DTO;
using th_poker_api.Model.Error;
using th_poker_api.Model.Success;
using th_poker_api.Model.Card;
using System.Data;
using th_poker_api.Model.Purchase;
using System.Drawing;
// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

/*rules of using seeding at this side

1. DO NOT USE REMOVE RANGE COMMAND UNLESS ITS REQUIRED
2. COMMENT ALL OF THE CODES THAT ALREADY GENERATED.
 */

namespace th_poker_api.Controllers
{
    public class SeederController : Controller
    {
        private Functions _funcs = new Functions();
        private MessageCodes _codes = new MessageCodes();
        private DataContext _context = new DataContext();

        /* [HttpPost("LoadRoles")]
         public async Task<Roles> LoadRoles(DeveloperDTO request)
         {
             Roles role = new Roles();
             try
             {
                 if (_funcs.checkDeveloperCode(request.developerCode) != true)
                 {
                     role.code = _codes.serverError.ToString();
                     role.message = "Invalid Developer Code";
                 }
                 else
                 {
                     var allRoles = _context.MDRoles.ToList();
                     _context.MDRoles.RemoveRange(allRoles);

                     MDRoles roleData = new MDRoles();
                     roleData.Desc = "Player";
                     roleData.CreatedBy = "SYSTEM";
                     roleData.CreatedOn = DateTime.Now;
                     roleData.UpdatedBy = "SYSTEM";
                     roleData.UpdatedOn = DateTime.Now;
                     _context.MDRoles.Add(roleData);

                     roleData = new MDRoles();
                     roleData.Desc = "ADMIN";
                     roleData.CreatedBy = "SYSTEM";
                     roleData.CreatedOn = DateTime.Now;
                     roleData.UpdatedBy = "SYSTEM";
                     roleData.UpdatedOn = DateTime.Now;
                     _context.MDRoles.Add(roleData);

                     roleData = new MDRoles();
                     roleData.Desc = "USER";
                     roleData.CreatedBy = "SYSTEM";
                     roleData.CreatedOn = DateTime.Now;
                     roleData.UpdatedBy = "SYSTEM";
                     roleData.UpdatedOn = DateTime.Now;
                     _context.MDRoles.Add(roleData);

                     _context.SaveChanges();
                     role.code = _codes.accepted.ToString();
                     role.message = "Data Successfully Reset";
                 }
             }
             catch (Exception err)
             {
                 role.code = _codes.error.ToString();
                 role.message = err.Message.ToString();
             }
             return role;
         }

         [HttpPost("LoadGameType")]
         public async Task<Roles> LoadGameType(DeveloperDTO request)
         {
             Roles role = new Roles();
             try
             {
                 if (_funcs.checkDeveloperCode(request.developerCode) != true)
                 {
                     role.code = _codes.serverError.ToString();
                     role.message = "Invalid Developer Code";
                 }
                 else
                 {
                     var allRoles = _context.MDGameType.ToList();
                     _context.MDGameType.RemoveRange(allRoles);
                     MDGameTypes roleData = new MDGameTypes();
                     roleData.IdMDGameType = 1.ToString();
                     roleData.GameDesc = "Poker";
                     roleData.UpdatedBy = "SYSTEM";
                     roleData.UpdatedOn = DateTime.Now;
                     _context.MDGameType.Add(roleData);

                     _context.SaveChanges();
                     role.code = _codes.accepted.ToString();
                     role.message = "Data Successfully Reset";
                 }
             }
             catch (Exception err)
             {
                 role.code = _codes.error.ToString();
                 role.message = err.Message.ToString();
             }
             return role;
         }

         [HttpPost("LoadStatuses")]
         public async Task<Roles> LoadStatuses(DeveloperDTO request)
         {
             Roles role = new Roles();
             try
             {
                 if (_funcs.checkDeveloperCode(request.developerCode) != true)
                 {
                     role.code = _codes.serverError.ToString();
                     role.message = "Invalid Developer Code";
                 }
                 else
                 {
                     var allRoles = _context.MDStatuses.ToList();
                     _context.MDStatuses.RemoveRange(allRoles);
                     List<MDStatuses> statuses = new List<MDStatuses>();

                     MDStatuses status = new MDStatuses();
                     status.Desc = "Active";
                     status.CreatedBy = "SYSTEM";
                     status.CreatedOn = DateTime.Now;
                     status.UpdatedBy = "SYSTEM";
                     status.UpdatedOn = DateTime.Now;
                     _context.MDStatuses.Add(status);

                     status = new MDStatuses();
                     status.Desc = "Banned";
                     status.CreatedBy = "SYSTEM";
                     status.CreatedOn = DateTime.Now;
                     status.UpdatedBy = "SYSTEM";
                     status.UpdatedOn = DateTime.Now;
                     _context.MDStatuses.Add(status);

                     status = new MDStatuses();
                     status.Desc = "PAID";
                     status.CreatedBy = "SYSTEM";
                     status.CreatedOn = DateTime.Now;
                     status.UpdatedBy = "SYSTEM";
                     status.UpdatedOn = DateTime.Now;
                     _context.MDStatuses.Add(status);

                     status = new MDStatuses();
                     status.Desc = "UNPAID";
                     status.CreatedBy = "SYSTEM";
                     status.CreatedOn = DateTime.Now;
                     status.UpdatedBy = "SYSTEM";
                     status.UpdatedOn = DateTime.Now;
                     _context.MDStatuses.Add(status);

                     status = new MDStatuses();
                     status.Desc = "HOLD";
                     status.CreatedBy = "SYSTEM";
                     status.CreatedOn = DateTime.Now;
                     status.UpdatedBy = "SYSTEM";
                     status.UpdatedOn = DateTime.Now;
                     _context.MDStatuses.Add(status);

                     _context.SaveChangesAsync();
                     role.code = _codes.accepted.ToString();
                     role.message = "Data Successfully Reset";
                 }
             }
             catch (Exception err)
             {
                 role.code = _codes.error.ToString();
                 role.message = err.Message.ToString();
             }
             return role;

         }

         [HttpPost("LoadGames")]
         public async Task<Roles> LoadGames(DeveloperDTO request)
         {
             Roles role = new Roles();
             try
             {
                 if (_funcs.checkDeveloperCode(request.developerCode) != true)
                 {
                     role.code = _codes.serverError.ToString();
                     role.message = "Invalid Developer Code";
                 }
                 else
                 {
                     var allroles = _context.MDGames.ToList();
                     _context.MDGames.RemoveRange(allroles);
                     MDGames roledata = new MDGames();
                     roledata.GameDesc = "casino \n singapore";
                     roledata.GameTitle = "marina bay sands";
                     roledata.IdMDGames = Guid.NewGuid().ToString();
                     roledata.BuyInMin = 2000;
                     roledata.BuyInMax = 10000;
                     roledata.StakesMin = 500;
                     roledata.StakesMax = 1000;
                     roledata.isActive = true;
                     roledata.UpdatedBy = "system";
                     roledata.UpdatedOn = DateTime.Now;
                     roledata.CreatedBy = "system";
                     roledata.CreatedOn = DateTime.Now;
                     _context.MDGames.Add(roledata);

                     roledata = new MDGames();
                     roledata.GameDesc = "casino \n lisboa";
                     roledata.GameTitle = "";
                     roledata.IdMDGames = Guid.NewGuid().ToString();
                     roledata.BuyInMin = 5000;
                     roledata.BuyInMax = 20000;
                     roledata.StakesMin = 800;
                     roledata.StakesMax = 1500;
                     roledata.isActive = true;
                     roledata.UpdatedBy = "system";
                     roledata.UpdatedOn = DateTime.Now;
                     roledata.CreatedBy = "system";
                     roledata.CreatedOn = DateTime.Now;
                     _context.MDGames.Add(roledata);

                     roledata = new MDGames();
                     roledata.GameDesc = "las vegas \n nevada";
                     roledata.GameTitle = "";
                     roledata.IdMDGames = Guid.NewGuid().ToString();
                     roledata.BuyInMin = 10000;
                     roledata.BuyInMax = 30000;
                     roledata.StakesMin = 1500;
                     roledata.StakesMax = 3000;
                     roledata.isActive = true;
                     roledata.UpdatedBy = "system";
                     roledata.UpdatedOn = DateTime.Now;
                     roledata.CreatedBy = "system";
                     roledata.CreatedOn = DateTime.Now;
                     _context.MDGames.Add(roledata);

                     _context.SaveChanges();
                     role.code = _codes.accepted.ToString();
                     role.message = "Data Successfully Reset";
                 }
             }
             catch (Exception err)
             {
                 role.code = _codes.error.ToString();
                 role.message = err.Message.ToString();
             }
             return role;
         }

         [HttpPost("Cards")]
         public async Task<Roles> Cards(DeveloperDTO request)
         {
             Roles role = new Roles();
             try
             {
                 if (_funcs.checkDeveloperCode(request.developerCode) != true)
                 {
                     role.code = _codes.serverError.ToString();
                     role.message = "Invalid Developer Code";
                 }
                 else
                 {
                     var Cards = _context.MDCards.ToList();
                     _context.MDCards.RemoveRange(Cards);

                     MDCards _cards = new MDCards();
                     _cards.IndexCard = 0;
                     _cards.Desc = "Clover 2";
                     _cards.CreatedOn = DateTime.Now;
                     _cards.UpdatedOn = DateTime.Now;
                     _cards.CreatedBy = "SYSTEMS";
                     _cards.UpdatedBy = "SYSTEMS";
                     _context.MDCards.Add(_cards);

                     _cards = new MDCards();
                     _cards.IndexCard = 1;
                     _cards.Desc = "Diamond 2";
                     _cards.CreatedOn = DateTime.Now;
                     _cards.UpdatedOn = DateTime.Now;
                     _cards.CreatedBy = "SYSTEMS";
                     _cards.UpdatedBy = "SYSTEMS";
                     _context.MDCards.Add(_cards);

                     _cards = new MDCards();
                     _cards.IndexCard = 2;
                     _cards.Desc = "Heart 2";
                     _cards.CreatedOn = DateTime.Now;
                     _cards.UpdatedOn = DateTime.Now;
                     _cards.CreatedBy = "SYSTEMS";
                     _cards.UpdatedBy = "SYSTEMS";
                     _context.MDCards.Add(_cards);

                     _cards = new MDCards();
                     _cards.IndexCard = 3;
                     _cards.Desc = "Spade 2";
                     _cards.CreatedOn = DateTime.Now;
                     _cards.UpdatedOn = DateTime.Now;
                     _cards.CreatedBy = "SYSTEMS";
                     _cards.UpdatedBy = "SYSTEMS";
                     _context.MDCards.Add(_cards);

                     _cards = new MDCards();
                     _cards.IndexCard = 4;
                     _cards.Desc = "Clover 3";
                     _cards.CreatedOn = DateTime.Now;
                     _cards.UpdatedOn = DateTime.Now;
                     _cards.CreatedBy = "SYSTEMS";
                     _cards.UpdatedBy = "SYSTEMS";
                     _context.MDCards.Add(_cards);

                     _cards = new MDCards();
                     _cards.IndexCard = 5;
                     _cards.Desc = "Diamond 3";
                     _cards.CreatedOn = DateTime.Now;
                     _cards.UpdatedOn = DateTime.Now;
                     _cards.CreatedBy = "SYSTEMS";
                     _cards.UpdatedBy = "SYSTEMS";
                     _context.MDCards.Add(_cards);

                     _cards = new MDCards();
                     _cards.IndexCard = 6;
                     _cards.Desc = "Heart 3";
                     _cards.CreatedOn = DateTime.Now;
                     _cards.UpdatedOn = DateTime.Now;
                     _cards.CreatedBy = "SYSTEMS";
                     _cards.UpdatedBy = "SYSTEMS";
                     _context.MDCards.Add(_cards);

                     _cards = new MDCards();
                     _cards.IndexCard = 7;
                     _cards.Desc = "Spade 3";
                     _cards.CreatedOn = DateTime.Now;
                     _cards.UpdatedOn = DateTime.Now;
                     _cards.CreatedBy = "SYSTEMS";
                     _cards.UpdatedBy = "SYSTEMS";
                     _context.MDCards.Add(_cards);

                     _cards = new MDCards();
                     _cards.IndexCard = 8;
                     _cards.Desc = "Clover 4";
                     _cards.CreatedOn = DateTime.Now;
                     _cards.UpdatedOn = DateTime.Now;
                     _cards.CreatedBy = "SYSTEMS";
                     _cards.UpdatedBy = "SYSTEMS";
                     _context.MDCards.Add(_cards);

                     _cards = new MDCards();
                     _cards.IndexCard = 9;
                     _cards.Desc = "Diamond 4";
                     _cards.CreatedOn = DateTime.Now;
                     _cards.UpdatedOn = DateTime.Now;
                     _cards.CreatedBy = "SYSTEMS";
                     _cards.UpdatedBy = "SYSTEMS";
                     _context.MDCards.Add(_cards);

                     _cards = new MDCards();
                     _cards.IndexCard = 10;
                     _cards.Desc = "Heart 4";
                     _cards.CreatedOn = DateTime.Now;
                     _cards.UpdatedOn = DateTime.Now;
                     _cards.CreatedBy = "SYSTEMS";
                     _cards.UpdatedBy = "SYSTEMS";
                     _context.MDCards.Add(_cards);

                     _cards = new MDCards();
                     _cards.IndexCard = 11;
                     _cards.Desc = "Spade 4";
                     _cards.CreatedOn = DateTime.Now;
                     _cards.UpdatedOn = DateTime.Now;
                     _cards.CreatedBy = "SYSTEMS";
                     _cards.UpdatedBy = "SYSTEMS";
                     _context.MDCards.Add(_cards);

                     _cards = new MDCards();
                     _cards.IndexCard = 12;
                     _cards.Desc = "Clover 5";
                     _cards.CreatedOn = DateTime.Now;
                     _cards.UpdatedOn = DateTime.Now;
                     _cards.CreatedBy = "SYSTEMS";
                     _cards.UpdatedBy = "SYSTEMS";
                     _context.MDCards.Add(_cards);

                     _cards = new MDCards();
                     _cards.IndexCard = 13;
                     _cards.Desc = "Diamond 5";
                     _cards.CreatedOn = DateTime.Now;
                     _cards.UpdatedOn = DateTime.Now;
                     _cards.CreatedBy = "SYSTEMS";
                     _cards.UpdatedBy = "SYSTEMS";
                     _context.MDCards.Add(_cards);

                     _cards = new MDCards();
                     _cards.IndexCard = 14;
                     _cards.Desc = "Heart 5";
                     _cards.CreatedOn = DateTime.Now;
                     _cards.UpdatedOn = DateTime.Now;
                     _cards.CreatedBy = "SYSTEMS";
                     _cards.UpdatedBy = "SYSTEMS";
                     _context.MDCards.Add(_cards);

                     _cards = new MDCards();
                     _cards.IndexCard = 15;
                     _cards.Desc = "Spade 5";
                     _cards.CreatedOn = DateTime.Now;
                     _cards.UpdatedOn = DateTime.Now;
                     _cards.CreatedBy = "SYSTEMS";
                     _cards.UpdatedBy = "SYSTEMS";
                     _context.MDCards.Add(_cards);

                     _cards = new MDCards();
                     _cards.IndexCard = 16;
                     _cards.Desc = "Clover 6";
                     _cards.CreatedOn = DateTime.Now;
                     _cards.UpdatedOn = DateTime.Now;
                     _cards.CreatedBy = "SYSTEMS";
                     _cards.UpdatedBy = "SYSTEMS";
                     _context.MDCards.Add(_cards);

                     _cards = new MDCards();
                     _cards.IndexCard = 17;
                     _cards.Desc = "Diamond 6";
                     _cards.CreatedOn = DateTime.Now;
                     _cards.UpdatedOn = DateTime.Now;
                     _cards.CreatedBy = "SYSTEMS";
                     _cards.UpdatedBy = "SYSTEMS";
                     _context.MDCards.Add(_cards);

                     _cards = new MDCards();
                     _cards.IndexCard = 18;
                     _cards.Desc = "Heart 6";
                     _cards.CreatedOn = DateTime.Now;
                     _cards.UpdatedOn = DateTime.Now;
                     _cards.CreatedBy = "SYSTEMS";
                     _cards.UpdatedBy = "SYSTEMS";
                     _context.MDCards.Add(_cards);

                     _cards = new MDCards();
                     _cards.IndexCard = 19;
                     _cards.Desc = "Spade 6";
                     _cards.CreatedOn = DateTime.Now;
                     _cards.UpdatedOn = DateTime.Now;
                     _cards.CreatedBy = "SYSTEMS";
                     _cards.UpdatedBy = "SYSTEMS";
                     _context.MDCards.Add(_cards);

                     _cards = new MDCards();
                     _cards.IndexCard = 20;
                     _cards.Desc = "Clover 7";
                     _cards.CreatedOn = DateTime.Now;
                     _cards.UpdatedOn = DateTime.Now;
                     _cards.CreatedBy = "SYSTEMS";
                     _cards.UpdatedBy = "SYSTEMS";
                     _context.MDCards.Add(_cards);

                     _cards = new MDCards();
                     _cards.IndexCard = 21;
                     _cards.Desc = "Diamond 7";
                     _cards.CreatedOn = DateTime.Now;
                     _cards.UpdatedOn = DateTime.Now;
                     _cards.CreatedBy = "SYSTEMS";
                     _cards.UpdatedBy = "SYSTEMS";
                     _context.MDCards.Add(_cards);

                     _cards = new MDCards();
                     _cards.IndexCard = 22;
                     _cards.Desc = "Heart 7";
                     _cards.CreatedOn = DateTime.Now;
                     _cards.UpdatedOn = DateTime.Now;
                     _cards.CreatedBy = "SYSTEMS";
                     _cards.UpdatedBy = "SYSTEMS";
                     _context.MDCards.Add(_cards);

                     _cards = new MDCards();
                     _cards.IndexCard = 23;
                     _cards.Desc = "Spade 7";
                     _cards.CreatedOn = DateTime.Now;
                     _cards.UpdatedOn = DateTime.Now;
                     _cards.CreatedBy = "SYSTEMS";
                     _cards.UpdatedBy = "SYSTEMS";
                     _context.MDCards.Add(_cards);

                     _cards = new MDCards();
                     _cards.IndexCard = 24;
                     _cards.Desc = "Clover 8";
                     _cards.CreatedOn = DateTime.Now;
                     _cards.UpdatedOn = DateTime.Now;
                     _cards.CreatedBy = "SYSTEMS";
                     _cards.UpdatedBy = "SYSTEMS";
                     _context.MDCards.Add(_cards);

                     _cards = new MDCards();
                     _cards.IndexCard = 25;
                     _cards.Desc = "Diamond 8";
                     _cards.CreatedOn = DateTime.Now;
                     _cards.UpdatedOn = DateTime.Now;
                     _cards.CreatedBy = "SYSTEMS";
                     _cards.UpdatedBy = "SYSTEMS";
                     _context.MDCards.Add(_cards);

                     _cards = new MDCards();
                     _cards.IndexCard = 26;
                     _cards.Desc = "Heart 8";
                     _cards.CreatedOn = DateTime.Now;
                     _cards.UpdatedOn = DateTime.Now;
                     _cards.CreatedBy = "SYSTEMS";
                     _cards.UpdatedBy = "SYSTEMS";
                     _context.MDCards.Add(_cards);

                     _cards = new MDCards();
                     _cards.IndexCard = 27;
                     _cards.Desc = "Spade 8";
                     _cards.CreatedOn = DateTime.Now;
                     _cards.UpdatedOn = DateTime.Now;
                     _cards.CreatedBy = "SYSTEMS";
                     _cards.UpdatedBy = "SYSTEMS";
                     _context.MDCards.Add(_cards);

                     _cards = new MDCards();
                     _cards.IndexCard = 28;
                     _cards.Desc = "Clover 9";
                     _cards.CreatedOn = DateTime.Now;
                     _cards.UpdatedOn = DateTime.Now;
                     _cards.CreatedBy = "SYSTEMS";
                     _cards.UpdatedBy = "SYSTEMS";
                     _context.MDCards.Add(_cards);

                     _cards = new MDCards();
                     _cards.IndexCard = 29;
                     _cards.Desc = "Diamond 9";
                     _cards.CreatedOn = DateTime.Now;
                     _cards.UpdatedOn = DateTime.Now;
                     _cards.CreatedBy = "SYSTEMS";
                     _cards.UpdatedBy = "SYSTEMS";
                     _context.MDCards.Add(_cards);

                     _cards = new MDCards();
                     _cards.IndexCard = 30;
                     _cards.Desc = "Heart 9";
                     _cards.CreatedOn = DateTime.Now;
                     _cards.UpdatedOn = DateTime.Now;
                     _cards.CreatedBy = "SYSTEMS";
                     _cards.UpdatedBy = "SYSTEMS";
                     _context.MDCards.Add(_cards);

                     _cards = new MDCards();
                     _cards.IndexCard = 31;
                     _cards.Desc = "Spade 9";
                     _cards.CreatedOn = DateTime.Now;
                     _cards.UpdatedOn = DateTime.Now;
                     _cards.CreatedBy = "SYSTEMS";
                     _cards.UpdatedBy = "SYSTEMS";
                     _context.MDCards.Add(_cards);

                     _cards = new MDCards();
                     _cards.IndexCard = 32;
                     _cards.Desc = "Clover 10";
                     _cards.CreatedOn = DateTime.Now;
                     _cards.UpdatedOn = DateTime.Now;
                     _cards.CreatedBy = "SYSTEMS";
                     _cards.UpdatedBy = "SYSTEMS";
                     _context.MDCards.Add(_cards);

                     _cards = new MDCards();
                     _cards.IndexCard = 33;
                     _cards.Desc = "Diamond 10";
                     _cards.CreatedOn = DateTime.Now;
                     _cards.UpdatedOn = DateTime.Now;
                     _cards.CreatedBy = "SYSTEMS";
                     _cards.UpdatedBy = "SYSTEMS";
                     _context.MDCards.Add(_cards);

                     _cards = new MDCards();
                     _cards.IndexCard = 34;
                     _cards.Desc = "Heart 10";
                     _cards.CreatedOn = DateTime.Now;
                     _cards.UpdatedOn = DateTime.Now;
                     _cards.CreatedBy = "SYSTEMS";
                     _cards.UpdatedBy = "SYSTEMS";
                     _context.MDCards.Add(_cards);

                     _cards = new MDCards();
                     _cards.IndexCard = 35;
                     _cards.Desc = "Spade 10";
                     _cards.CreatedOn = DateTime.Now;
                     _cards.UpdatedOn = DateTime.Now;
                     _cards.CreatedBy = "SYSTEMS";
                     _cards.UpdatedBy = "SYSTEMS";
                     _context.MDCards.Add(_cards);

                     _cards = new MDCards();
                     _cards.IndexCard = 36;
                     _cards.Desc = "Clover A";
                     _cards.CreatedOn = DateTime.Now;
                     _cards.UpdatedOn = DateTime.Now;
                     _cards.CreatedBy = "SYSTEMS";
                     _cards.UpdatedBy = "SYSTEMS";
                     _context.MDCards.Add(_cards);

                     _cards = new MDCards();
                     _cards.IndexCard = 37;
                     _cards.Desc = "Diamond A";
                     _cards.CreatedOn = DateTime.Now;
                     _cards.UpdatedOn = DateTime.Now;
                     _cards.CreatedBy = "SYSTEMS";
                     _cards.UpdatedBy = "SYSTEMS";
                     _context.MDCards.Add(_cards);

                     _cards = new MDCards();
                     _cards.IndexCard = 38;
                     _cards.Desc = "Heart A";
                     _cards.CreatedOn = DateTime.Now;
                     _cards.UpdatedOn = DateTime.Now;
                     _cards.CreatedBy = "SYSTEMS";
                     _cards.UpdatedBy = "SYSTEMS";
                     _context.MDCards.Add(_cards);

                     _cards = new MDCards();
                     _cards.IndexCard = 39;
                     _cards.Desc = "Spade A";
                     _cards.CreatedOn = DateTime.Now;
                     _cards.UpdatedOn = DateTime.Now;
                     _cards.CreatedBy = "SYSTEMS";
                     _cards.UpdatedBy = "SYSTEMS";
                     _context.MDCards.Add(_cards);

                     _cards = new MDCards();
                     _cards.IndexCard = 40;
                     _cards.Desc = "Clover J";
                     _cards.CreatedOn = DateTime.Now;
                     _cards.UpdatedOn = DateTime.Now;
                     _cards.CreatedBy = "SYSTEMS";
                     _cards.UpdatedBy = "SYSTEMS";
                     _context.MDCards.Add(_cards);

                     _cards = new MDCards();
                     _cards.IndexCard = 41;
                     _cards.Desc = "Diamond J";
                     _cards.CreatedOn = DateTime.Now;
                     _cards.UpdatedOn = DateTime.Now;
                     _cards.CreatedBy = "SYSTEMS";
                     _cards.UpdatedBy = "SYSTEMS";
                     _context.MDCards.Add(_cards);

                     _cards = new MDCards();
                     _cards.IndexCard = 42;
                     _cards.Desc = "Heart J";
                     _cards.CreatedOn = DateTime.Now;
                     _cards.UpdatedOn = DateTime.Now;
                     _cards.CreatedBy = "SYSTEMS";
                     _cards.UpdatedBy = "SYSTEMS";
                     _context.MDCards.Add(_cards);

                     _cards = new MDCards();
                     _cards.IndexCard = 43;
                     _cards.Desc = "Spade J";
                     _cards.CreatedOn = DateTime.Now;
                     _cards.UpdatedOn = DateTime.Now;
                     _cards.CreatedBy = "SYSTEMS";
                     _cards.UpdatedBy = "SYSTEMS";
                     _context.MDCards.Add(_cards);

                     _cards = new MDCards();
                     _cards.IndexCard = 44;
                     _cards.Desc = "Clover Q";
                     _cards.CreatedOn = DateTime.Now;
                     _cards.UpdatedOn = DateTime.Now;
                     _cards.CreatedBy = "SYSTEMS";
                     _cards.UpdatedBy = "SYSTEMS";
                     _context.MDCards.Add(_cards);

                     _cards = new MDCards();
                     _cards.IndexCard = 45;
                     _cards.Desc = "Diamond Q";
                     _cards.CreatedOn = DateTime.Now;
                     _cards.UpdatedOn = DateTime.Now;
                     _cards.CreatedBy = "SYSTEMS";
                     _cards.UpdatedBy = "SYSTEMS";
                     _context.MDCards.Add(_cards);

                     _cards = new MDCards();
                     _cards.IndexCard = 46;
                     _cards.Desc = "Heart Q";
                     _cards.CreatedOn = DateTime.Now;
                     _cards.UpdatedOn = DateTime.Now;
                     _cards.CreatedBy = "SYSTEMS";
                     _cards.UpdatedBy = "SYSTEMS";
                     _context.MDCards.Add(_cards);

                     _cards = new MDCards();
                     _cards.IndexCard = 47;
                     _cards.Desc = "Spade Q";
                     _cards.CreatedOn = DateTime.Now;
                     _cards.UpdatedOn = DateTime.Now;
                     _cards.CreatedBy = "SYSTEMS";
                     _cards.UpdatedBy = "SYSTEMS";
                     _context.MDCards.Add(_cards);

                     _cards = new MDCards();
                     _cards.IndexCard = 48;
                     _cards.Desc = "Clover K";
                     _cards.CreatedOn = DateTime.Now;
                     _cards.UpdatedOn = DateTime.Now;
                     _cards.CreatedBy = "SYSTEMS";
                     _cards.UpdatedBy = "SYSTEMS";
                     _context.MDCards.Add(_cards);

                     _cards = new MDCards();
                     _cards.IndexCard = 49;
                     _cards.Desc = "Diamond K";
                     _cards.CreatedOn = DateTime.Now;
                     _cards.UpdatedOn = DateTime.Now;
                     _cards.CreatedBy = "SYSTEMS";
                     _cards.UpdatedBy = "SYSTEMS";
                     _context.MDCards.Add(_cards);

                     _cards = new MDCards();
                     _cards.IndexCard = 50;
                     _cards.Desc = "Heart K";
                     _cards.CreatedOn = DateTime.Now;
                     _cards.UpdatedOn = DateTime.Now;
                     _cards.CreatedBy = "SYSTEMS";
                     _cards.UpdatedBy = "SYSTEMS";
                     _context.MDCards.Add(_cards);

                     _cards = new MDCards();
                     _cards.IndexCard = 51;
                     _cards.Desc = "Spade K";
                     _cards.CreatedOn = DateTime.Now;
                     _cards.UpdatedOn = DateTime.Now;
                     _cards.CreatedBy = "SYSTEMS";
                     _cards.UpdatedBy = "SYSTEMS";
                     _context.MDCards.Add(_cards);

                     _context.SaveChanges();
                     role.code = _codes.accepted.ToString();
                     role.message = "Data Successfully Reset";
                 }
             }
             catch (Exception err)
             {
                 role.code = _codes.error.ToString();
                 role.message = err.Message.ToString();
             }
             return role;
         }

         [HttpPost("winType")]
         public async Task<Roles> winType(DeveloperDTO request)
         {
             Roles role = new Roles();
             try
             {
                 if (!_funcs.checkDeveloperCode(request.developerCode))
                 {
                     role.code = _codes.serverError.ToString();
                     role.message = "Invalid Developer Code";
                 }
                 else
                 {
                     var _allWinType = _context.WinType.ToList();
                     _context.WinType.RemoveRange(_allWinType);
                     WinType win = new WinType();
                     win.IdWinType = 0;
                     win.Desc = "High Card"; //Royal Flush
                     win.CreatedBy = "System";
                     win.CreatedOn = DateTime.Now;
                     _context.Add(win);

                     win = new WinType();
                     win.IdWinType = 1;
                     win.Desc = "Pair"; //Straight Flush
                     win.CreatedBy = "System";
                     win.CreatedOn = DateTime.Now;
                     _context.Add(win);

                     win = new WinType();
                     win.IdWinType = 2;
                     win.Desc = "Two pair"; // Four of a Kind
                     win.CreatedBy = "System";
                     win.CreatedOn = DateTime.Now;
                     _context.Add(win);

                     win = new WinType();
                     win.IdWinType = 3;
                     win.Desc = "Three of a Kind"; //Full House
                     win.CreatedBy = "System";
                     win.CreatedOn = DateTime.Now;
                     _context.Add(win);

                     win = new WinType();
                     win.IdWinType = 4;
                     win.Desc = "Straight"; //Flush
                     win.CreatedBy = "System";
                     win.CreatedOn = DateTime.Now;
                     _context.Add(win);

                     win = new WinType();
                     win.IdWinType = 5;
                     win.Desc = "Flush"; //Straight
                     win.CreatedBy = "System";
                     win.CreatedOn = DateTime.Now;
                     _context.Add(win);

                     win = new WinType();
                     win.IdWinType = 6;
                     win.Desc = "Full House"; //Three of a Kind
                     win.CreatedBy = "System";
                     win.CreatedOn = DateTime.Now;
                     _context.Add(win);

                     win = new WinType();
                     win.IdWinType = 7;
                     win.Desc = "Four of a Kind"; //Two Pair
                     win.CreatedBy = "System";
                     win.CreatedOn = DateTime.Now;
                     _context.Add(win);

                     win = new WinType();
                     win.IdWinType = 8;
                     win.Desc = "Straight Flush"; // Pair
                     win.CreatedBy = "System";
                     win.CreatedOn = DateTime.Now;
                     _context.Add(win);

                     win = new WinType();
                     win.IdWinType = 9;
                     win.Desc = "Royal Flush"; //High Card
                     win.CreatedBy = "System";
                     win.CreatedOn = DateTime.Now;
                     _context.Add(win);

                     _context.SaveChanges();
                     role.code = _codes.accepted.ToString();
                     role.message = "Data Successfully Reset";
                 }
             }
             catch (Exception err)
             {
                 role.code = _codes.error.ToString();
                 role.message = err.Message.ToString();
             }
             return role;
         }

         #region List Currency

         [HttpPost("LoadCurrency")]
         public async Task<Roles> LoadCurrency(DeveloperDTO request)
         {
             Roles role = new Roles();
             try
             {
                 if (!_funcs.checkDeveloperCode(request.developerCode))
                 {
                     role.code = _codes.serverError.ToString();
                     role.message = "Invalid Developer Code";
                 }
                 else
                 {
                     var _allCurrency = _context.MDPaymentItem.ToList();
                     _context.MDPaymentItem.RemoveRange(_allCurrency);
                     MDPaymentItem _item = new MDPaymentItem();
                     _item.IdPymItem = Guid.NewGuid().ToString();
                     _item.Value = 10;
                     _item.Price = 50;
                     _item.Desc = "Bronze";
                     _item.CreatedBy = "System";
                     _item.CreatedOn = DateTime.Now;
                     _context.Add(_item);

                     _item = new MDPaymentItem();
                     _item.IdPymItem = Guid.NewGuid().ToString();
                     _item.Value = 20;
                     _item.Price = 100;
                     _item.Desc = "Silver";
                     _item.CreatedBy = "System";
                     _item.CreatedOn = DateTime.Now;
                     _context.Add(_item);

                     _item = new MDPaymentItem();
                     _item.IdPymItem = Guid.NewGuid().ToString();
                     _item.Value = 30;
                     _item.Price = 150;
                     _item.Desc = "Gold";
                     _item.CreatedBy = "System";
                     _item.CreatedOn = DateTime.Now;
                     _context.Add(_item);

                     _item = new MDPaymentItem();
                     _item.IdPymItem = Guid.NewGuid().ToString();
                     _item.Value = 40;
                     _item.Price = 200;
                     _item.Desc = "Diamond";
                     _item.CreatedBy = "System";
                     _item.CreatedOn = DateTime.Now;
                     _context.Add(_item);

                     _item = new MDPaymentItem();
                     _item.IdPymItem = Guid.NewGuid().ToString();
                     _item.Value = 50;
                     _item.Price = 250;
                     _item.Desc = "Platinum";
                     _item.CreatedBy = "System";
                     _item.CreatedOn = DateTime.Now;
                     _context.Add(_item);

                     _item = new MDPaymentItem();
                     _item.IdPymItem = Guid.NewGuid().ToString();
                     _item.Value = 60;
                     _item.Price = 300;
                     _item.Desc = "Legend";
                     _item.CreatedBy = "System";
                     _item.CreatedOn = DateTime.Now;
                     _context.Add(_item);

                     _item = new MDPaymentItem();
                     _item.IdPymItem = Guid.NewGuid().ToString();
                     _item.Value = 100;
                     _item.Price = 500;
                     _item.Desc = "Mythic";
                     _item.CreatedBy = "System";
                     _item.CreatedOn = DateTime.Now;
                     _context.Add(_item);

                     _context.SaveChanges();
                     role.code = _codes.accepted.ToString();
                     role.message = "Data Successfully Reset";
                 }
             }
             catch (Exception err)
             {
                 role.code = _codes.error.ToString();
                 role.message = err.Message.ToString();
             }
             return role;
         }
         #endregion*/
    }
}

