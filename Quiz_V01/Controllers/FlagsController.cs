using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using Antlr.Runtime;
using Microsoft.Ajax.Utilities;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Quiz_V01.Models;

namespace Quiz_V01.Controllers
{
    public class FlagsController : Controller
    {
        private static Random rd = new Random();

        string[] countries = {"Brasil","Canadá","China","Estados Unidos","França","Israel","Itália","Ruanda","Coréia do Sul","Inglaterra"};

        string json = @"[
    {
      'flagId': 1,
      'flagName': 'Brasil',
      'flagUrl': '/Resources/Images/flag_brazil.png',
      'flagValue': 1
    },
    {
      'flagId': 2,
      'flagName': 'Canadá',
      'flagUrl': '/Resources/Images/flag_canada.png',
      'flagValue': 3
    },
    {
      'flagId': 3,
      'flagName': 'China',
      'flagUrl': '/Resources/Images/flag_china.png',
      'flagValue': 2
    },
    {
      'flagId': 4,
      'flagName': 'Estados Unidos',
      'flagUrl': '/Resources/Images/flag_eua.png',
      'flagValue': 5
    },
    {
      'flagId': 5,
      'flagName': 'França',
      'flagUrl': '/Resources/Images/flag_france.png',
      'flagValue': 3
    },
    {
      'flagId': 6,
      'flagName': 'Israel',
      'flagUrl': '/Resources/Images/flag_israel.png',
      'flagValue': 2
    },
    {
      'flagId': 7,
      'flagName': 'Itália',
      'flagUrl': '/Resources/Images/flag_italy.png',
      'flagValue': 1
    },
    {
      'flagId': 8,
      'flagName': 'Ruanda',
      'flagUrl': '/Resources/Images/flag_rwanda.png',
      'flagValue': 4
    },
    {
      'flagId': 9,
      'flagName': 'Coréia do Sul',
      'flagUrl': '/Resources/Images/flag_sk.png',
      'flagValue': 2
    },
    {
      'flagId': 10,
      'flagName': 'Inglaterra',
      'flagUrl': '/Resources/Images/flag_uk.png',
      'flagValue': 4
    }
]
";

        public List<Flag> ShuffleCollection(List<Flag> flags)
        {
            return flags.OrderBy(a => Guid.NewGuid()).ToList();
        }

        public List<Flag> GetAllFlags()
        {
            var flags1 = JsonConvert.DeserializeObject<List<Flag>>(json);
            return flags1;
        }

        public Flag GetFirstFlag(List<Flag> flags)
        {
            return flags.FirstOrDefault();
        }

        public Flag GetFlagById(int? id)
        {
            var flagsCollection = GetAllFlags();

            return flagsCollection.FirstOrDefault(f => f.IdFlag == id);
        }

        public List<string> ShuffleStrings(List<string> strings)
        {
            return strings.OrderBy(order => Guid.NewGuid()).ToList();
        }

        public List<string> GetCountriesForQuiz(List<Flag> flags)
        {
            var countriesForQuiz = new List<string>();
            rd.Next(1, flags.Count);

            var firstCountry = flags.FirstOrDefault();

            countriesForQuiz.Add(firstCountry.FlagName);

            var countriesWithoutCurrent = CollectionWithoutCurrentFlag(firstCountry.FlagName, flags);

            for (int i = 0; i < 3; i++)
            {
                countriesForQuiz.Add(countriesWithoutCurrent[i].FlagName);
            }

            return ShuffleStrings(countriesForQuiz);

        }

        public Flag GetCurrentFlag(List<Flag> flags)
        {
            return flags.FirstOrDefault();
        }

        public List<Flag> CollectionWithoutCurrentFlag(string flag, List<Flag> flags)
        {
            return flags.Where(f => f.FlagName != flag).ToList();
        }

        // GET: Flags
        public ActionResult Index()
        {
            //Takes collection from Json and shuffles
            var flags = GetAllFlags();

            var shuffledflags = ShuffleCollection(flags);

            FlagViewModel flagsvm = new FlagViewModel();
            flagsvm.flag = GetFirstFlag(shuffledflags);
            flagsvm.AllFlags = shuffledflags;
            flagsvm.FlagsToQuestion = GetCountriesForQuiz(shuffledflags);

            return View(flagsvm);
        }

        public ActionResult Result()
        {
            return View();
        }

        // GET: Flags/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Flag flag = GetFlagById(id);
            if (flag == null)
            {
                return HttpNotFound();
            }
            return View(flag);
        }

        // GET: Flags/Create
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CheckResponse(string currentFlagName, string response, List<Flag> currentCollection)
        {
            string check = String.Empty;

            if (response == currentFlagName)
            {
                check = "Resposta correta :)";
            }
            else
            {
                check = "Resposta incorreta :|";
            }

            var newCollection = ShuffleCollection(CollectionWithoutCurrentFlag(currentFlagName, currentCollection));

            FlagViewModel fvm = new FlagViewModel();
            fvm.flag = GetCurrentFlag(newCollection);
            fvm.AnswerCheck = check;
            fvm.AllFlags = newCollection;
            fvm.FlagsToQuestion = GetCountriesForQuiz(newCollection);

            var result = JsonConvert.SerializeObject(fvm);

            return Json(result);

        }
    }
}
