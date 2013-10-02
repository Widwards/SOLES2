using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using SOLES2.Models.ViewModels;
using SOLES2.Infrastructure;
using SOLES2.Models;
using Newtonsoft.Json;

namespace SOLES2.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/
        [HttpGet]
        public ActionResult Index()
        {
            return View("LinEq");
        }

        [HttpPost]
        public JsonResult Index(LinEq linEq)
        {
            var info = new LinEqInfo();
            var parser = new LinEqParser(linEq.Expression);
            try
            {
                parser.Initialize();
                info.IsSuccess = true;
            }
            catch (Exception e)
            {
                info.IsSuccess = false;
                
                if (e is ArgumentNullException)
                {
                    info.ErrorMessage = "You have not entered the equation!";
                }
                else
                {
                    info.ErrorMessage = e.Message;
                }
            }
            var solver = new LinEqSolver(parser.MM, parser.Vector, parser.SystemSize);
            double[] vectorSol = new double[parser.SystemSize];
            info.IsXForm = parser.IsXForm;

            if (info.IsSuccess)
            {
                try
                {
                    vectorSol = solver.Solve();
                    info.IsSuccess = true;


                }
                
                catch (Exception e)
                {
                    info.IsSuccess = false;
                    info.ErrorMessage = e.Message;
                }
                
            }
            
            if (info.IsSuccess)
            {
                SolutionBuilder sb = new SolutionBuilder(vectorSol, info.IsXForm);
                info.Solution = sb.Build();
            }
            //string pars = Parser.Parse(linEq.Expression);
            //Matrix m = Matrix.Parse(pars);
            //ViewBag.Re = pars;


            var json = JsonConvert.SerializeObject(info);
            return Json(json);
        }

        [HttpGet]
        public ActionResult test()
        {

            return View();
        }
        [HttpPost]
        public ActionResult test(LinEq linEq)
        {
            var info = new LinEqInfo();
            var parser = new LinEqParser(linEq.Expression);
            try
            {
                parser.Initialize();
                info.IsSuccess = true;
            }
            catch (Exception e)
            {
                info.IsSuccess = false;
                info.ErrorMessage = e.Message;
            }
            var solver = new LinEqSolver(parser.MM, parser.Vector, parser.SystemSize);
            double[] vectorSol = new double[parser.SystemSize];

            if (info.IsSuccess)
            {
                try
                {
                    vectorSol = solver.Solve();
                    info.IsSuccess = true;

                }
                catch (Exception e)
                {
                    info.IsSuccess = false;
                    info.ErrorMessage = e.Message;
                }
            }
            
            if (info.IsSuccess)
            {
                SolutionBuilder sb = new SolutionBuilder(vectorSol, info.IsXForm);
                info.Solution = sb.Build();
             }

            return View();
        }

    }
}
