using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;
using Foundation.Commerce.Models;
using Sitecore.Mvc.Controllers;
using Foundation.Security.Attributes;
using Foundation.Commerce;
using Foundation.Commerce.Extensions;
using Foundation.Commerce.Managers;
using Foundation.Commerce.Models.InputModels;
using Foundation.Commerce.Util;
using Sitecore.Diagnostics;

namespace Feature.TestSimulation.Website.Controller
{
    public class TestSimulationController : SitecoreController
    {
        private CartManager CartManager { get; }
        private CommerceUserContext CommerceUserContext { get; }

        public TestSimulationController(CartManager cartManager, CommerceUserContext commerceUserContext)
        {
            CartManager = cartManager;
            CommerceUserContext = commerceUserContext;
        }

        /// <summary>
        /// Setups the test simulation instance.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ValidateJsonAntiForgeryToken]
        public JsonResult Setup()
        {
            try
            {
                var response = CartManager.TestSimulationSetUp(CommerceUserContext.Current.UserId);
                var result = new BaseApiModel();

                if (!response)
                {
                    result.SetError("Test Simulation Failed!");
                }

                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json(new ErrorApiModel("Setup", e), JsonRequestBehavior.AllowGet);
            }
        }
    }
}