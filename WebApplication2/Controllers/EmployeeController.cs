

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Eng360Web.Models.Domain;
using Eng360Web.Models.ViewModel;
using AutoMapper;
using Eng360Web.Models.Service.Imp;
using Eng360Web.Models.Utility;
using System.Globalization;
using Eng360Web.Models.Security;

namespace Eng360Web.Controllers
{
    [AccessDeniedAuthorize]
    public class EmployeeController : SuperBaseController
    {
        private Eng360Entities1 db = new Eng360Entities1();

        private readonly IEmployeeServices employeeService;
        private readonly ISupplierServices supplierService;



        public EmployeeController(IEmployeeServices _employeeService, ISupplierServices _supplierService)
        {
            employeeService = _employeeService;
            supplierService = _supplierService;



        }

        // GET: Employee
        public ActionResult Index()
        {
            //var eng_employee_profile = db.eng_employee_profile.Include(e => e.eng_address_master).Include(e => e.eng_usergroup);
            //return View(eng_employee_profile.ToList());
            return View(employeeService.getAllEmployees().Where(a => a.IsActive == 1).ToList());
        }


        // GET: Employee
        public ActionResult ReportIndex()
        {
            var filter = new FilterViewModel();
            var emp = employeeService.getAllEmployees().Where(a => a.IsActive == 1).ToList();

            var fn = employeeService.getAllEmployees().Where(a => a.IsActive == 1).ToList();
            fn.Insert(0, new EmployeeViewModel() { UserID = 0, FullName = "Select" });

            //var ob = employeeService.getAllEmployees().GroupBy(a => a.OpBranch).ToList();
            var ob = employeeService.getAllEmployees().Select(a => a.OpBranch).Distinct().ToList();
            ob.Insert(0, "Select");


            ViewBag.UserID = new SelectList(fn, "UserID", "FullName");
            ViewBag.OpBranch = new SelectList(ob);
            // ViewBag.OpBranch = new SelectList(employeeService.getAllEmployees().Select(a=>a.OpBranch).Distinct());
            //emplist.Insert(0, new ClientContactViewModel() { CCID = 0, SPOCName = "Select", eng_client_master = new ClientViewModel() { ClientID = 0 } });

            filter.eng_employee_master = emp;


            return View(filter);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ReportIndex(FilterViewModel filter)
        {
            if (ModelState.IsValid)
            {
                var fil = new FilterViewModel();
                var fn = employeeService.getAllEmployees().Where(a => a.IsActive == 1).ToList();
                fn.Insert(0, new EmployeeViewModel() { UserID = 0, FullName = "Select" });

                //var ob = employeeService.getAllEmployees().GroupBy(a => a.OpBranch).ToList();
                
                if (!string.IsNullOrEmpty(filter.dateFrom))
                    filter.dateFrom = DateTime.ParseExact(filter.dateFrom, "dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("MM/dd/yyyy");
                if (!string.IsNullOrEmpty(filter.dateTo))
                    filter.dateTo = DateTime.ParseExact(filter.dateTo, "dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("MM/dd/yyyy");


                var emp = employeeService.getFilterEmployees(filter).ToList();
                fil.eng_employee_master = emp;
                ViewBag.UserID = new SelectList(fn, "UserID", "FullName", fil.UserID);
                fil.dateFrom = filter.dateFrom;
                fil.dateTo = filter.dateTo;
                
                return View(fil);

            }
            return View();

        }

        public ActionResult EMPDetails(int emp)
        {
            string fname = (string)employeeService.getEmployee(emp).FirstName;
            //   eng_product_master eng_product_master = db.eng_product_master.Find(id);
            string lname = (string)employeeService.getEmployee(emp).LastName;
            string mail = (string)employeeService.getEmployee(emp).eng_address_master.Email;
            int group = (int)employeeService.getEmployee(emp).GroupID;
            var GRPDetails = supplierService.getGroup(group);
            string groupname = GRPDetails.GroupName;

            return Json(new
            {
                value = "OK",
                fname = fname,
                lname =lname,
                mail=mail,
                groupid = groupname

            });
        }



        // GET: Employee/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //eng_employee_profile eng_employee_profile = db.eng_employee_profile.Find(id);
            var employee = employeeService.getEmployee(id ?? default(int));
            if (employee == null)
            {
                return HttpNotFound();
            }

            ViewBag.ContactID = new SelectList(employeeService.getAddresses(), "ContactID", "Email1", employee.AddressID);
            ViewBag.JobGroup = supplierService.getAllGroups().Where(a => a.GroupID == employee.GroupID).Select(a => a.GroupName).FirstOrDefault();
            ViewBag.Country = supplierService.getAllcountry().Where(a => a.id == employee.Country).Select(a => a.name).FirstOrDefault();
            return View(employee);
        }

        // GET: Employee/Create
        public ActionResult Create()
        {

            //  ViewBag.ContactID = new SelectList(db.eng_contact_master, "ContactID", "Email1");
            var Group = employeeService.getUsergroups().Where(a => a.GroupID != 1 && a.IsActive == 1).ToList();
            Group.Insert(0, new GroupViewModel() { GroupID = 0, GroupName = "-Select-" });
            //Group.Insert(0, "-Select-");
            ViewBag.GroupID = new SelectList(Group,"GroupID", "GroupName");
            //var ReportMan = employeeService.getAllEmployees().Where(a => a.GroupID == 2 && a.IsActive == 1).Select(a => a.FullName).ToList();
            //ReportMan.Insert(0, "-Select-");
            //ViewBag.ReportManager = new SelectList(ReportMan);
            var ReportMan = employeeService.getAllEmployees().Where(a => a.GroupID == 2 && a.IsActive == 1).ToList();
            ReportMan.Insert(0, new EmployeeViewModel() { UserID = 0, FullName = "-Select-" });
            ViewBag.ReportManager = new SelectList(ReportMan, "UserID", "FullName");
            var country_list = supplierService.getAllcountry().ToList();
            country_list.Insert(0, new countryViewModel() { id = 0, name = "-Select-" });
            ViewBag.Country = new SelectList(country_list, "id", "name");
            return View();
        }

        // POST: Employee/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        //public ActionResult Create([Bind(Include = "UserID,UserName,Password,EmpID,OpBranch,GroupID,FirstName,MidName,LastName,DisplayName,ContactID,Job_Title,DoB,DoJ,DoR,Gender,Designation,ID_Type,ID_Number,Profile_Desc,Profile_Photo_Path,llevel,CreatedDate,DeletedDate,CreatedBy,DeletedBy,LastLogin,Passport_Number,Passport_Valid_Till,Permit_Number,Permit_Valid_From,Permit_Valid_To,Licence_Number,Licence_Valid_Till,Insurance_Number,Insurance_Valid_Till,IsActive")] EmployeeViewModel eng_employee_profile)
        public ActionResult Create(EmployeeViewModel eng_employee_profile)
        {
            //if (ModelState.IsValid )
            //{
            //   db.eng_employee_profile.Add(eng_employee_profile);
            //  db.SaveChanges();

            if (!string.IsNullOrEmpty(eng_employee_profile.DoB))
                eng_employee_profile.DoB = DateTime.ParseExact(eng_employee_profile.DoB, "dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("dd/MM/yyyy");
            if (!string.IsNullOrEmpty(eng_employee_profile.DoJ))
                eng_employee_profile.DoJ = DateTime.ParseExact(eng_employee_profile.DoJ, "dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("dd/MM/yyyy");
            if (!string.IsNullOrEmpty(eng_employee_profile.DoR))
                eng_employee_profile.DoR = DateTime.ParseExact(eng_employee_profile.DoR, "dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("dd/MM/yyyy");
            if (!string.IsNullOrEmpty(eng_employee_profile.Passport_Valid_Till))
                eng_employee_profile.Passport_Valid_Till = DateTime.ParseExact(eng_employee_profile.Passport_Valid_Till, "dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("dd/MM/yyyy");
            if (!string.IsNullOrEmpty(eng_employee_profile.Permit_Valid_From))
                eng_employee_profile.Permit_Valid_From = DateTime.ParseExact(eng_employee_profile.Permit_Valid_From, "dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("dd/MM/yyyy");
            if (!string.IsNullOrEmpty(eng_employee_profile.Permit_Valid_To))
                eng_employee_profile.Permit_Valid_To = DateTime.ParseExact(eng_employee_profile.Permit_Valid_To, "dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("dd/MM/yyyy");
            if (!string.IsNullOrEmpty(eng_employee_profile.Licence_Valid_Till))
                eng_employee_profile.Licence_Valid_Till = DateTime.ParseExact(eng_employee_profile.Licence_Valid_Till, "dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("dd/MM/yyyy");
            if (!string.IsNullOrEmpty(eng_employee_profile.Insurance_Valid_Till))
                eng_employee_profile.Insurance_Valid_Till = DateTime.ParseExact(eng_employee_profile.Insurance_Valid_Till, "dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("dd/MM/yyyy");
            if (!string.IsNullOrEmpty(eng_employee_profile.SOC_Issue_Date))
                eng_employee_profile.SOC_Issue_Date = DateTime.ParseExact(eng_employee_profile.SOC_Issue_Date, "dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("dd/MM/yyyy");
            if (!string.IsNullOrEmpty(eng_employee_profile.SOC_Expiry_Date))
                eng_employee_profile.SOC_Expiry_Date = DateTime.ParseExact(eng_employee_profile.SOC_Expiry_Date, "dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("dd/MM/yyyy");
            if (!string.IsNullOrEmpty(eng_employee_profile.License_Scissor_Lift_ExpiryDate))
                eng_employee_profile.License_Scissor_Lift_ExpiryDate = DateTime.ParseExact(eng_employee_profile.License_Scissor_Lift_ExpiryDate, "dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("dd/MM/yyyy");
            if (!string.IsNullOrEmpty(eng_employee_profile.License_Boom_Lift_ExpiryDate))
                eng_employee_profile.License_Boom_Lift_ExpiryDate = DateTime.ParseExact(eng_employee_profile.License_Boom_Lift_ExpiryDate, "dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("dd/MM/yyyy");
            if (!string.IsNullOrEmpty(eng_employee_profile.License_WorkatHeight_ExpiryDate))
                eng_employee_profile.License_WorkatHeight_ExpiryDate = DateTime.ParseExact(eng_employee_profile.License_WorkatHeight_ExpiryDate, "dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("dd/MM/yyyy");
            if (!string.IsNullOrEmpty(eng_employee_profile.License_IslandPass_ExpiryDate))
                eng_employee_profile.License_IslandPass_ExpiryDate = DateTime.ParseExact(eng_employee_profile.License_IslandPass_ExpiryDate, "dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("dd/MM/yyyy");
            if (!string.IsNullOrEmpty(eng_employee_profile.License_Course_Expiry_Date))
                eng_employee_profile.License_Course_Expiry_Date = DateTime.ParseExact(eng_employee_profile.License_Course_Expiry_Date, "dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("dd/MM/yyyy");


            eng_employee_profile.CreatedBy = AppSession.GetCurrentUserId();
            eng_employee_profile.CreatedDate = DateTime.Now;
            eng_employee_profile.IsActive = 1;
            if(eng_employee_profile.ReportManager =="0")
            {
                eng_employee_profile.Report_Manager = null;
                eng_employee_profile.ReportManager = null;
            }
            else
            {
                var id = Int16.Parse(eng_employee_profile.ReportManager);
                eng_employee_profile.Report_Manager = id;
                var Report = employeeService.getEmployee(id);
                Report.FullName = Report.FirstName + " " + Report.LastName;
                eng_employee_profile.ReportManager = Report.FullName;
            }

            var reuslt = employeeService.CreateEmployee(eng_employee_profile);

            if (reuslt > 0)
            {
                return getSuccessfulOperation();
            }
            else
            {
                return getFailedOperation();
            }


            //}

            //ViewBag.ContactID = new SelectList(db.eng_address_master, "ContactID", "Email1", eng_employee_profile.AddressID);
            //ViewBag.Group_ID = new SelectList(employeeService.getUsergroups(), "GroupID", "GroupName", eng_employee_profile.GroupID);
            // eng_employee_profile.Group_ID = new SelectList(db.eng_usergroup, "GroupID", "GroupName", eng_employee_profile.GroupID);
            //  return View(eng_employee_profile);
            return RedirectToAction("Create");
        }

        // GET: Employee/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //eng_employee_profile eng_employee_profile = db.eng_employee_profile.Find(id);
            var employee = employeeService.getEmployee(id ?? default(int));
            var Report = employeeService.getAllEmployees().Where(a => a.UserID == employee.Report_Manager).Select(a => a.UserID).SingleOrDefault();
            if (employee == null)
            {
                return HttpNotFound();
            }
            ViewBag.ContactID = new SelectList(employeeService.getAddresses(), "ContactID", "Email1", employee.AddressID);
            ViewBag.GroupID = new SelectList(employeeService.getUsergroups().Where(a => a.GroupID != 1 && a.IsActive==1).ToList(), "GroupID", "GroupName", employee.GroupID);
            //var employee = Mapper.Map<EmployeeViewModel>(eng_employee_profile);
            if (employee.GroupID==2)
            {
                var ReportMan = employeeService.getAllEmployees().Where(a => a.GroupID == 2 && a.IsActive == 1 && a.UserID != employee.UserID).ToList();
                if (employee.ReportManager == null)
                {
                    ReportMan.Insert(0, new EmployeeViewModel() { UserID = 0, FullName = "-Select-" });
                    ViewBag.ReportManager = new SelectList(ReportMan, "UserID", "FullName");
                }
                else
                {
                    ReportMan.Insert(0, new EmployeeViewModel() { UserID = 0, FullName = "-Select-" });
                    ViewBag.ReportManager = new SelectList(ReportMan, "UserID", "FullName", Report);
                }
            }
            else
            {
                var ReportMan = employeeService.getAllEmployees().Where(a => a.GroupID == 2 && a.IsActive == 1).ToList();
                if (employee.ReportManager == null)
                {
                    ReportMan.Insert(0, new EmployeeViewModel() { UserID = 0, FullName = "-Select-" });
                    ViewBag.ReportManager = new SelectList(ReportMan, "UserID", "FullName");
                }
                else
                {
                    ReportMan.Insert(0, new EmployeeViewModel() { UserID = 0, FullName = "-Select-" });
                    ViewBag.ReportManager = new SelectList(ReportMan, "UserID", "FullName", Report);
                }
            }
            
            
            
            var Country = supplierService.getAllcountry().ToList();
            ViewBag.Country = new SelectList(Country, "id", "name", employee.Country);
            ViewBag.gender = employee.Gender;
            return View(employee);
        }

        // POST: Employee/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(EmployeeViewModel eng_employee_profile)
        {
            //if (ModelState.IsValid)
            //{

            if (!string.IsNullOrEmpty(eng_employee_profile.DoB))
                eng_employee_profile.DoB = DateTime.ParseExact(eng_employee_profile.DoB, "dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("dd/MM/yyyy");
            if (!string.IsNullOrEmpty(eng_employee_profile.DoJ))
                eng_employee_profile.DoJ = DateTime.ParseExact(eng_employee_profile.DoJ, "dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("dd/MM/yyyy");
            if (!string.IsNullOrEmpty(eng_employee_profile.DoR))
                eng_employee_profile.DoR = DateTime.ParseExact(eng_employee_profile.DoR, "dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("dd/MM/yyyy");
            if (!string.IsNullOrEmpty(eng_employee_profile.Passport_Valid_Till))
                eng_employee_profile.Passport_Valid_Till = DateTime.ParseExact(eng_employee_profile.Passport_Valid_Till, "dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("dd/MM/yyyy");
            if (!string.IsNullOrEmpty(eng_employee_profile.Permit_Valid_From))
                eng_employee_profile.Permit_Valid_From = DateTime.ParseExact(eng_employee_profile.Permit_Valid_From, "dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("MM/dd/yyyy");
            if (!string.IsNullOrEmpty(eng_employee_profile.Permit_Valid_To))
                eng_employee_profile.Permit_Valid_To = DateTime.ParseExact(eng_employee_profile.Permit_Valid_To, "dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("MM/dd/yyyy");
            if (!string.IsNullOrEmpty(eng_employee_profile.Licence_Valid_Till))
                eng_employee_profile.Licence_Valid_Till = DateTime.ParseExact(eng_employee_profile.Licence_Valid_Till, "dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("MM/dd/yyyy");
            if (!string.IsNullOrEmpty(eng_employee_profile.Insurance_Valid_Till))
                eng_employee_profile.Insurance_Valid_Till = DateTime.ParseExact(eng_employee_profile.Insurance_Valid_Till, "dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("MM/dd/yyyy");
            if (!string.IsNullOrEmpty(eng_employee_profile.SOC_Issue_Date))
                eng_employee_profile.SOC_Issue_Date = DateTime.ParseExact(eng_employee_profile.SOC_Issue_Date, "dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("MM/dd/yyyy");
            if (!string.IsNullOrEmpty(eng_employee_profile.SOC_Expiry_Date))
                eng_employee_profile.SOC_Expiry_Date = DateTime.ParseExact(eng_employee_profile.SOC_Expiry_Date, "dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("MM/dd/yyyy");
            if (!string.IsNullOrEmpty(eng_employee_profile.License_Scissor_Lift_ExpiryDate))
                eng_employee_profile.License_Scissor_Lift_ExpiryDate = DateTime.ParseExact(eng_employee_profile.License_Scissor_Lift_ExpiryDate, "dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("MM/dd/yyyy");
            if (!string.IsNullOrEmpty(eng_employee_profile.License_Boom_Lift_ExpiryDate))
                eng_employee_profile.License_Boom_Lift_ExpiryDate = DateTime.ParseExact(eng_employee_profile.License_Boom_Lift_ExpiryDate, "dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("MM/dd/yyyy");
            if (!string.IsNullOrEmpty(eng_employee_profile.License_WorkatHeight_ExpiryDate))
                eng_employee_profile.License_WorkatHeight_ExpiryDate = DateTime.ParseExact(eng_employee_profile.License_WorkatHeight_ExpiryDate, "dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("MM/dd/yyyy");
            if (!string.IsNullOrEmpty(eng_employee_profile.License_IslandPass_ExpiryDate))
                eng_employee_profile.License_IslandPass_ExpiryDate = DateTime.ParseExact(eng_employee_profile.License_IslandPass_ExpiryDate, "dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("MM/dd/yyyy");
            if (!string.IsNullOrEmpty(eng_employee_profile.License_Course_Expiry_Date))
                eng_employee_profile.License_Course_Expiry_Date = DateTime.ParseExact(eng_employee_profile.License_Course_Expiry_Date, "dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("MM/dd/yyyy");

            eng_employee_profile.UpdatedBy = AppSession.GetCurrentUserId();
            eng_employee_profile.UpdatedDate = DateTime.Now;

            if (eng_employee_profile.ReportManager == "0")
            {
                eng_employee_profile.Report_Manager = null;
                eng_employee_profile.ReportManager = null;
            }
            else
            {
                var id = Int16.Parse(eng_employee_profile.ReportManager);
                eng_employee_profile.Report_Manager = id;
                var Report = employeeService.getEmployee(id);
                Report.FullName = Report.FirstName + " " + Report.LastName;
                eng_employee_profile.ReportManager = Report.FullName;
            }
            var result = employeeService.SaveEmployee(eng_employee_profile);
            if (result > 0)
            {

                return getSuccessfulOperation();
            }
            else
            {
                return getFailedOperation();
            }

            // db.Entry(eng_employee_profile).State = EntityState.Modified;
            // db.SaveChanges();

            //}

            ViewBag.ContactID = new SelectList(employeeService.getAddresses(), "ContactID", "Email1", eng_employee_profile.AddressID);
            //ViewBag.GroupID = new SelectList(employeeService.getUsergroups(), "GroupID", "GroupName", eng_employee_profile.GroupID);
            return View(eng_employee_profile);
        }

        // GET: Employee/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            eng_employee_profile eng_employee_profile = db.eng_employee_profile.Find(id);
            if (eng_employee_profile == null)
            {
                return HttpNotFound();
            }
            return View(eng_employee_profile);
        }

        // POST: Employee/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {


            var result = employeeService.DeleteEmployee(id);

            //eng_employee_profile eng_employee_profile = db.eng_employee_profile.Find(id);
            //db.eng_employee_profile.Remove(eng_employee_profile);
            // var result =     db.SaveChanges();

            if (result > 0)
            {
                return getSuccessfulOperation();
            }
            else
            {
                return getFailedOperation();
            }
            // return RedirectToAction("Index");
        }

        [ValidateAntiForgeryToken]
        public ActionResult getAllEmployees()
        {
            var empNames = employeeService.getAllEmployees().Where(a => a.IsActive == 1).ToList().Select(e => e.FirstName + ' ' + e.LastName).ToList();
            return Json(empNames);

        }

        // GET: Employee/Dashboard
        //public ActionResult GetHRDashBoard()
        //{
        //    var EmpReminder = employeeService.getHREmpDashBoardResults().ToList();
        //    return View(EmpReminder);
        //}

        // GET: Employee/Dashboard
        //public ActionResult GetCompanyDashBoard()
        //{
        //    var CpyReminder = employeeService.getCompanyDashBoardResults().ToList();
        //    return View(CpyReminder);
        //}

        // GET: Employee/Dashboard
        //public ActionResult GetVehicleDashBoard()
        //{
        //    var VehReminder = employeeService.getVehicleDashBoardResults().ToList();
        //    return View(VehReminder);
        //}

        // GET: Payable/Dashboard
        //public ActionResult GetPayableDashBoard()
        //{
        //    var PayReminder = employeeService.getPayableDashBoardResults().ToList();
        //    return View(PayReminder);
        //}

        // GET: Receivable/Dashboard
        //public ActionResult GetReceivableDashBoard()
        //{
        //    var RecReminder = employeeService.getReceivableDashBoardResults().ToList();
        //    return View(RecReminder);
        //}

        // GET: Operation/Dashboard
        //public ActionResult GetOperationDashBoard()
        //{
        //    var OpReminder = employeeService.getOperationDashBoardResults().ToList();
        //    return View(OpReminder);
        //}

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}