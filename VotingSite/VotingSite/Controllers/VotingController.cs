using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;


namespace VotingSite.Controllers
{
    public class VotingController : AsyncController
    {
        // GET: Voting
        [HttpGet]
        [Authorize]
        public async Task<ActionResult> Index()
        {
            await Task.Delay(1);

            return View();
        }
    }
}
