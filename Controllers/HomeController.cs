using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Dojodachi.Models;
using Microsoft.AspNetCore.Http;

namespace Dojodachi.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        [Route("")]
        public IActionResult Index()
        {
            if (HttpContext.Session.GetInt32("Fullness") == null)
            {
                HttpContext.Session.SetInt32("Fullness", 20);
            }
            if (HttpContext.Session.GetInt32("Happiness") == null)
            {
                HttpContext.Session.SetInt32("Happiness", 20);
            }
            if (HttpContext.Session.GetInt32("Meals") == null)
            {
                HttpContext.Session.SetInt32("Meals", 3);
            }
            if (HttpContext.Session.GetInt32("Energy") == null)
            {
                HttpContext.Session.SetInt32("Energy", 50);
            }
            if (HttpContext.Session.GetString("Status") == null)
            {
                HttpContext.Session.SetString("Status", "Ready to Play?");
            }

            int FullnessVar = (int)HttpContext.Session.GetInt32("Fullness");
            ViewBag.Fullness = FullnessVar;
            int HappinessVar = (int)HttpContext.Session.GetInt32("Happiness");
            ViewBag.Happiness = HappinessVar;
            int MealsVar = (int)HttpContext.Session.GetInt32("Meals");
            ViewBag.Meals = MealsVar;
            int EnergyVar = (int)HttpContext.Session.GetInt32("Energy");
            ViewBag.Energy = HttpContext.Session.GetInt32("Energy");

            if (EnergyVar > 100 && HappinessVar > 100 && FullnessVar > 100)
            {
                HttpContext.Session.SetString("Status", "Congratulations! You Won!");
            }
            ViewBag.Status = HttpContext.Session.GetString("Status");

            return View();
        }

        [HttpGet]
        [Route("feed")]
        public IActionResult Feed()
        {
            int MealsVar = (int)HttpContext.Session.GetInt32("Meals");
            int FullnessVar = (int)HttpContext.Session.GetInt32("Fullness");
            Random random = new Random();
            int randFullness = random.Next(5, 11);
            Random random2 = new Random();
            int badChance = random2.Next(0, 4);
            if (MealsVar > 0)
            {
                if (badChance == 2)
                {
                    HttpContext.Session.SetInt32("Meals", MealsVar - 1);
                    HttpContext.Session.SetString("Status", $"BAD LUCK! You lost a meal, but did not gain fullness!");
                }
                else
                {
                    HttpContext.Session.SetInt32("Meals", MealsVar - 1);
                    HttpContext.Session.SetInt32("Fullness", FullnessVar + randFullness);
                    int NewMealsVar = (int)HttpContext.Session.GetInt32("Meals");
                    int NewFullnessVar = (int)HttpContext.Session.GetInt32("Fullness");
                    int MealsChange = NewMealsVar - MealsVar;
                    int FullnessChange = NewFullnessVar - FullnessVar;
                    HttpContext.Session.SetString("Status", $"This boi is eating. Fullness +{FullnessChange}, Meals {MealsChange}");

                }
            }
            else
            {
                HttpContext.Session.SetString("Status", "Not enough meals!");
            }
            return RedirectToAction("Index");

        }

        [HttpGet]
        [Route("play")]
        public IActionResult Play()
        {
            int EnergyVar = (int)HttpContext.Session.GetInt32("Energy");
            int HappinessVar = (int)HttpContext.Session.GetInt32("Happiness");
            Random random = new Random();
            int randHappiness = random.Next(5, 11);
            Random random2 = new Random();
            int badChance = random2.Next(0, 4);
            if (EnergyVar > 0)
            {
                if (badChance == 2)
                {
                    HttpContext.Session.SetInt32("Energy", EnergyVar - 5);
                    HttpContext.Session.SetString("Status", $"BAD LUCK! You lost energy, but did not gain happiness!");
                }
                else
                {
                    HttpContext.Session.SetInt32("Energy", EnergyVar - 5);
                    HttpContext.Session.SetInt32("Happiness", HappinessVar + randHappiness);
                    int NewEnergyVar = (int)HttpContext.Session.GetInt32("Energy");
                    int NewHappinessVar = (int)HttpContext.Session.GetInt32("Happiness");
                    int EnergyChange = NewEnergyVar - EnergyVar;
                    int HappinessChange = NewHappinessVar - HappinessVar;
                    HttpContext.Session.SetString("Status", $"This boi is playing. Happiness +{HappinessChange}, Energy {EnergyChange}");
                }
            }
            else
            {
                HttpContext.Session.SetString("Status", "Not enough energy!");
            }
            return RedirectToAction("Index");
        }

        [HttpGet]
        [Route("work")]
        public IActionResult Work()
        {
            int EnergyVar = (int)HttpContext.Session.GetInt32("Energy");
            int MealsVar = (int)HttpContext.Session.GetInt32("Meals");
            Random random = new Random();
            int randMeals = random.Next(1, 4);
            if (EnergyVar > 5)
            {
                HttpContext.Session.SetInt32("Energy", EnergyVar - 5);
                HttpContext.Session.SetInt32("Meals", MealsVar + randMeals);
                int NewMealsVar = (int)HttpContext.Session.GetInt32("Meals");
                int NewEnergyVar = (int)HttpContext.Session.GetInt32("Energy");
                int MealsChange = NewMealsVar - MealsVar;
                int EnergyChange = NewEnergyVar - EnergyVar;
                HttpContext.Session.SetString("Status", $"This boi is working. Meals +{MealsChange}, Energy {EnergyChange}");
            }
            else
            {
                HttpContext.Session.SetString("Status", "Not enough energy!");
            }
            return RedirectToAction("Index");
        }

        [HttpGet]
        [Route("sleep")]
        public IActionResult Sleep()
        {
            int EnergyVar = (int)HttpContext.Session.GetInt32("Energy");
            int HappinessVar = (int)HttpContext.Session.GetInt32("Happiness");
            int FullnessVar = (int)HttpContext.Session.GetInt32("Fullness");
            if (HappinessVar >= 5 && FullnessVar >= 5)
            {
                HttpContext.Session.SetInt32("Energy", EnergyVar + 15);
                HttpContext.Session.SetInt32("Fullness", FullnessVar - 5);
                HttpContext.Session.SetInt32("Happiness", HappinessVar - 5);
                int NewEnergyVar = (int)HttpContext.Session.GetInt32("Energy");
                int NewFullnessVar = (int)HttpContext.Session.GetInt32("Fullness");
                int NewHappinessVar = (int)HttpContext.Session.GetInt32("Happiness");
                int EnergyChange = NewEnergyVar - EnergyVar;
                int FullnessChange = NewFullnessVar - FullnessVar;
                int HappinessChange = NewHappinessVar - HappinessVar;
                HttpContext.Session.SetString("Status", $"This boi is sleeping. Energy +{EnergyChange}, Fullness {FullnessChange}, Happiness {HappinessChange}");
            }
            else
            {
                HttpContext.Session.SetString("Status", "Not enough fullness or happiness!");
            }
            return RedirectToAction("Index");
        }

        [HttpGet]
        [Route("restart")]
        public IActionResult Restart()
        {
            int EnergyVar = (int)HttpContext.Session.GetInt32("Energy");
            int MealsVar = (int)HttpContext.Session.GetInt32("Meals");
            int HappinessVar = (int)HttpContext.Session.GetInt32("Happiness");
            int FullnessVar = (int)HttpContext.Session.GetInt32("Fullness");
            HttpContext.Session.SetInt32("Energy", 50);
            HttpContext.Session.SetInt32("Meals", 3);
            HttpContext.Session.SetInt32("Happiness", 20);
            HttpContext.Session.SetInt32("Fullness", 20);
            HttpContext.Session.SetString("Status", "Ready to Play?");
            return RedirectToAction("Index");
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

