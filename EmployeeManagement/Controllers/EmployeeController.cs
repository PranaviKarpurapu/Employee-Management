using EmployeeManagement.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace EmployeeManagement.Controler
{
    public class EmployeeController : Controller
    {
        /*    IEmployeeRepository er=new EmployeeRepository();     */

        private readonly IEmployeeRepository employeeRepository;
        //dependency injected object
        //By declaring readonly we can only assign the variable once in constructor
        public EmployeeController(IEmployeeRepository empRepository)
        {
            employeeRepository = empRepository;
        }

        // Model 1: Passing our own Id i.e., 1

        /* public string Search()
         {
             Employee emp= er.GetEmployee(1);
             return emp.Id + "\n" + emp.Name + "\n" + emp.Email + " \n" + emp.Dept;
         }
         */



        //  Model 2: We are passing id value as parameter which also accepts nullable value
        public IActionResult Search(int? id)
        {
            int ID = (int)((id == null) ? 1 : id);
            Employee emp = employeeRepository.GetEmployee(ID);
            //if(emp!=null)
            //{ 
            //return Content(emp.Id + "\n" + emp.Name + "\n" + emp.Email + " \n" + emp.Dept);
            //}
            //return Content("Employee does not exist with this Id");



            //**********************************************************************
            // Model 1: ViewData acts as kety value pairs


            //ViewData["id"] = emp.Id;
            //ViewData["name"] = emp.Name;
            //ViewData["email"] = emp.Email;
            //ViewData["dept"] = emp.Dept;

            //************************

            //Model 1.1 By passing object as property

            //ViewData["employee"]=emp;


            //return View();

            //**********************************************************************
            //Model 2: ViewBag


            //ViewBag.id = emp.Id;
            //ViewBag.name = emp.Name;
            //ViewBag.email = emp.Email;
            //ViewBag.dept = emp.Dept;


            //************************

            //Model 2.1 By passing object as property


            //ViewBag.employee = emp;
            //return View();



            //**********************************************************************
            //Model 3: Strongly Typed

            //Here ; Object is passed and it available on the view page

            return View(emp);


        }
        public IActionResult Index()
        {
            //return "Hi........!!!!!    from MVC Index";

            //for retrieving all list values we are passing list object 

            List<Employee> elist = employeeRepository.DisplayDetails();
            return View(elist);

        }

        //***************************************************************************

        // Example for View Model
        public ViewResult AboutEmployee()
        {
            Employee emp = employeeRepository.GetEmployee(2);

            /*
              ViewBag.projectName = "BookHive";
              return View(emp);
            */

            EmployeeProjectViewModel ep = new EmployeeProjectViewModel();
            ep.employee = emp;
            ep.projectName = "BookHive";

            return View(ep);
        }

        //***************************************************************************

        // Example for navigating to another view instead of default view
        public IActionResult GetAllEmployees()
        {
            //************************

            //Model 1 : Returns all values

            //List<Employee> elist = employeeRepository.DisplayDetails();

            ////We return view by writing absolute path view

            //return View("~/Views/Employee/Index.cshtml",elist);

            //************************

            //Model 2 : Returns only the matched values

            //List<Employee> elist = (employeeRepository.DisplayDetails()).Where(e => e.Dept == "CSE").ToList();
            //return View("~/Views/Employee/Index.cshtml", elist);


            //************************

            //Model 3 : Relative Path to refer a view i.e., if the view is in same controller we can directly give the method name as below

            List<Employee> elist = (employeeRepository.DisplayDetails()).Where(e => e.Dept == Dept.CSE).ToList();
            return View("Index", elist);


            //************************

            //Model 4 :if the view is in another controller ,the syntax is return View("../Another Controller Name/Index", elist);
        }


        [HttpGet]
        public IActionResult Create()
        {
            //Employee emp = employeeRepository.GetEmployee(2);
            //return View(emp);
            return View();
        }

        [HttpPost]
        public IActionResult Create(Employee emp)
        {
            if(ModelState.IsValid)
            {
                employeeRepository.AddEmployee(emp);
                return RedirectToAction("Index");
            }
            //else
            //return View("Fail");
            return View();
        }

        //After Submitting it gives message
        public IActionResult Fail()
        {
            return View();
        }

        public IActionResult Success()
        {
            return View();
        }

        public IActionResult List()
        {
            List<Employee> elist = employeeRepository.DisplayDetails();
                return View(elist);
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(Employee e)
        {
            bool res = employeeRepository.AddEmployee(e);
            //return View();
            return RedirectToAction("Index");
        }


        //********************************************************************************

            //****For checking alraedy same mail exist or not
        public bool IsExist(string email)
        {
            var result = (employeeRepository.DisplayDetails()).Find(r => r.Email == email);
            if (result == null)
                return true;
            else
                return false;
        }


        [AcceptVerbs("Get","Post")]
        
        public JsonResult IsEmailExist(string email)
        {
            return IsExist(email) ? Json(true) : Json("Email already exists");
        }


        //********************************************************************************

        //****For checking alraedy same name exist or not

        public bool IsExist1(string name)
        {
            var result = (employeeRepository.DisplayDetails()).Find(r => r.Name == name);
            if (result == null)
                return true;
            else
                return false;
        }


        [AcceptVerbs("Get", "Post")]
       
        public JsonResult IsNameExist(string name)
        {
            return IsExist1(name) ? Json(true) : Json("This name already exists");
        }


        public IActionResult _myPartial()
        {
            return View();
        }
    }
}

 
