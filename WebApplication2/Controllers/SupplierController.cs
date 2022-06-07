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
using Eng360Web.Models.Service.Interface;
using Eng360Web.Models.Utility;
using Eng360Web.Models.Security;
using System.Globalization;

namespace Eng360Web.Controllers
{
    [AccessDeniedAuthorize]
    public class SupplierController : SuperBaseController
    {
        private Eng360Entities1 db = new Eng360Entities1();

        private readonly ISupplierServices supplierService;
        private readonly IEmployeeServices employeeService;
        private readonly IClientServices clientService;
        private readonly IUserServices userServices;

        public SupplierController(ISupplierServices _supplierService, IEmployeeServices _employeeService, IClientServices _clientService, IUserServices _userServices)
        {
            supplierService = _supplierService;
            employeeService = _employeeService;
            clientService = _clientService;
            userServices = _userServices;
        }

        // GET: Supplier
        public ActionResult Index()
        {
            return View(supplierService.getAllSuppliers().Where(a => a.IsActive == 1).ToList());
        }


        // GET: Supplier
        public ActionResult ReportIndex()
        {
            var filter = new FilterViewModel();
            var supplier = supplierService.getAllSuppliers().Where(a => a.IsActive == 1).ToList();

            var suppname = supplierService.getAllSuppliers().Where(a => a.IsActive == 1).Select(a => a.Company_Name).Distinct().ToList();
            suppname.Insert(0, "Select");

            ViewBag.Company_Name = new SelectList(suppname);

            filter.eng_supplier_master = supplier;

            return View(filter);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ReportIndex(FilterViewModel filter)
        {
            if (ModelState.IsValid)
            {
                var fil = new FilterViewModel();

                var suppname = supplierService.getAllSuppliers().Where(a => a.IsActive == 1).Select(a => a.Company_Name).Distinct().ToList();
                suppname.Insert(0, "Select");

                if (filter.Company_Name != "Select")
                {
                    var supplier = supplierService.getAllSuppliers().Where(a => a.IsActive == 1 && a.Company_Name == filter.Company_Name).ToList();
                    fil.eng_supplier_master = supplier;
                    ViewBag.Company_Name = new SelectList(suppname, filter.Company_Name);
                }
                else
                {
                    var supplier = supplierService.getAllSuppliers().Where(a => a.IsActive == 1).ToList();
                    fil.eng_supplier_master = supplier;
                    ViewBag.Company_Name = new SelectList(suppname);
                }

                return View(fil);

            }
            return View();

        }


        // GET: Supplier/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //eng_supplier_master eng_supplier_master = db.eng_supplier_master.Find(id);
            var supplier = supplierService.getSupplier(id ?? default(int));
            if (supplier == null)
            {
                return HttpNotFound();
            }
            ViewBag.IndustryID = new SelectList(clientService.getAllIndustries(), "Id", "Fn_title", supplier.IndustryID);
            ViewBag.AddressID = new SelectList(employeeService.getAddresses(), "AddressID", "Email", supplier.AddressID);
            return View(supplier);
        }

        // GET: Supplier/Create
        public ActionResult Create()
        {
            //var supplier = new SupplierViewModel();
            ViewBag.IndustryID = new SelectList(clientService.getAllIndustries(), "Id", "Industry_Title");
            return View();
        }

        // POST: Supplier/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(SupplierViewModel eng_supplier_master)
        {

            if (ModelState.IsValid)
            {
                eng_supplier_master.CreatedBy = AppSession.GetCurrentUserId();
                eng_supplier_master.CreatedDate = DateTime.Now;
                eng_supplier_master.IsActive = 1;

                var result = supplierService.CreateSupplier(eng_supplier_master);

                if (result > 0)
                {
                    return getSuccessfulOperation();
                }
                else
                {
                    return getFailedOperation();
                }

            }


            return View(eng_supplier_master);
        }

        // GET: Supplier/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //  eng_supplier_master eng_supplier_master = db.eng_supplier_master.Find(id);
            var supplier = supplierService.getSupplier(id ?? default(int));
            if (supplier == null)
            {
                return HttpNotFound();
            }
            ViewBag.AddressID = new SelectList(employeeService.getAddresses(), "AddressID", "Email", supplier.AddressID);
            ViewBag.IndustryID = new SelectList(clientService.getAllIndustries(), "Id", "Industry_Title", supplier.IndustryID);
            return View(supplier);
        }

        // POST: Supplier/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(SupplierViewModel eng_supplier_master)
        {
            if (ModelState.IsValid)
            {
                eng_supplier_master.UpdatedBy = AppSession.GetCurrentUserId();
                eng_supplier_master.UpdatedDate = DateTime.Now;

                var result = supplierService.SaveSupplier(eng_supplier_master);
                if (result > 0)
                {

                    return getSuccessfulOperation();
                }
                else
                {
                    return getFailedOperation();
                }

            }
            return View(eng_supplier_master);
        }

        // GET: Supplier/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            eng_supplier_master eng_supplier_master = db.eng_supplier_master.Find(id);
            if (eng_supplier_master == null)
            {
                return HttpNotFound();
            }
            return View(eng_supplier_master);
        }

        // POST: Supplier/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {

            var result = supplierService.DeleteSupplier(id);

            if (result > 0)
            {
                return getSuccessfulOperation();
            }
            else
            {
                return getFailedOperation();
            }
        }

        #region Leave settings

        public ActionResult LevIndex()
        {
            return View(supplierService.getAllLeaveSettings().Where(a => a.IsActive == 1).ToList());
        }

        public ActionResult LevCreate()
        {
            //var supplier = new SupplierViewModel();
            //ViewBag.IndustryID = new SelectList(clientService.getAllIndustries(), "Id", "Industry_Title");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LevCreate(LeaveSettingViewModel eng_leave_setting)

        {

            if (ModelState.IsValid)
            {
                eng_leave_setting.CreatedBy = userServices.getAllUsers().Where(a => a.UserID == AppSession.GetCurrentUserId()).Select(a => a.EmpID).FirstOrDefault();
                eng_leave_setting.CreatedDate = DateTime.Now;
                eng_leave_setting.IsActive = 1;
                
                var result = supplierService.CreateLeaveSetting(eng_leave_setting);

                if (result > 0)
                {
                    return getSuccessfulOperation();
                }
                else
                {
                    return getFailedOperation();
                }

            }


            return View(eng_leave_setting);
        }


        public ActionResult LevDelete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            eng_leave_settings eng_leave_settings = db.eng_leave_settings.Find(id);
            if (eng_leave_settings == null)
            {
                return HttpNotFound();
            }
            return View(eng_leave_settings);
        }

        // POST: Supplier/Delete/5
        [HttpPost, ActionName("LevDelete")]
        [ValidateAntiForgeryToken]
        public ActionResult LevDeleteConfirmed(int id)
        {

            var result = supplierService.DeleteLeaveSetting(id);

            if (result > 0)
            {
                return getSuccessfulOperation();
            }
            else
            {
                return getFailedOperation();
            }
        }


        public ActionResult LevDetails(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //eng_supplier_master eng_supplier_master = db.eng_supplier_master.Find(id);
            var Leave = supplierService.getLeaveSetting(id ?? default(int));
            if (Leave == null)
            {
                return HttpNotFound();
            }
            Leave.eng_employee_profile1.FullName= Leave.eng_employee_profile1.FirstName + ' ' + Leave.eng_employee_profile1.LastName+"("+Leave.eng_employee_profile1.EmpID+")";
            if(Leave.eng_employee_profile!=null)
            {
                Leave.eng_employee_profile.FullName = Leave.eng_employee_profile.FirstName + ' ' + Leave.eng_employee_profile.LastName + "(" + Leave.eng_employee_profile.EmpID + ")";

            }
            return View(Leave);
        }

        public ActionResult LevEdit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //  eng_supplier_master eng_supplier_master = db.eng_supplier_master.Find(id);
            var leave = supplierService.getLeaveSetting(id ?? default(int));
            if (leave == null)
            {
                return HttpNotFound();
            }

            return View(leave);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LevEdit(LeaveSettingViewModel eng_leave_setting)
        {
            //if (ModelState.IsValid)
            //{
            eng_leave_setting.UpdatedBy = userServices.getAllUsers().Where(a => a.UserID == AppSession.GetCurrentUserId()).Select(a => a.EmpID).FirstOrDefault(); ;
            eng_leave_setting.UpdatedDate = DateTime.Now;

            var result = supplierService.SaveLeaveSetting(eng_leave_setting);
            if (result > 0)
            {

                return getSuccessfulOperation();
            }
            else
            {
                return getFailedOperation();
            }

            //}
            //return View(eng_leave_setting);
        }

        #endregion
        #region User Group

        public ActionResult GroupIndex()
        {
            return View(supplierService.getAllGroups().Where(a => a.IsActive == 1).ToList());
        }


        public ActionResult GroupDelete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            eng_usergroup eng_usergroup = db.eng_usergroup.Find(id);
            if (eng_usergroup == null)
            {
                return HttpNotFound();
            }
            return View(eng_usergroup);
        }

        // POST: Supplier/Delete/5
        [HttpPost, ActionName("GroupDelete")]
        [ValidateAntiForgeryToken]
        public ActionResult GroupDeleteConfirmed(int id)
        {

            var result = supplierService.DeleteGroup(id);

            if (result > 0)
            {
                return getSuccessfulOperation();
            }
            else
            {
                return getFailedOperation();
            }
        }

        public ActionResult GroupDetails(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //eng_supplier_master eng_supplier_master = db.eng_supplier_master.Find(id);
            var Group = supplierService.getGroup(id ?? default(int));
            if (Group == null)
            {
                return HttpNotFound();
            }
            var update = employeeService.getAllEmployees().Where(a => a.UserID == Group.UpdatedBy).FirstOrDefault();
            if (update!=null)
            {
                Group.UpdatedByName = update.FirstName + ' ' + update.LastName + "(" + update.EmpID + ")";
            }
            
            
            var create = employeeService.getAllEmployees().Where(a => a.UserID == Group.CreatedBy).FirstOrDefault();
            Group.CreatedByName = create.FirstName + ' ' + create.LastName + "(" + create.EmpID + ")";
            return View(Group);
        }
        public ActionResult GroupCreate()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult GroupCreate(GroupViewModel eng_usergroup)

        {

            if (ModelState.IsValid)
            {
                eng_usergroup.CreatedBy = userServices.getAllUsers().Where(a => a.UserID == AppSession.GetCurrentUserId()).Select(a => a.EmpID).FirstOrDefault();
                eng_usergroup.CreatedDate = DateTime.Now;

                eng_usergroup.IsActive = 1;

                var result = supplierService.CreateGroup(eng_usergroup);

                if (result > 0)
                {
                    return getSuccessfulOperation();
                }
                else
                {
                    return getFailedOperation();
                }

            }


            return View(eng_usergroup);
        }

        public ActionResult  GroupEdit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //  eng_supplier_master eng_supplier_master = db.eng_supplier_master.Find(id);
            var group = supplierService.getGroup(id ?? default(int));
            if (group == null)
            {
                return HttpNotFound();
            }

            return View(group);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult GroupEdit(GroupViewModel eng_usergroup)
        {
            //if (ModelState.IsValid)
            //{
            eng_usergroup.UpdatedBy = userServices.getAllUsers().Where(a => a.UserID == AppSession.GetCurrentUserId()).Select(a => a.EmpID).FirstOrDefault(); ;
            eng_usergroup.UpdatedDate = DateTime.Now;

            var result = supplierService.SaveGroup(eng_usergroup);
            if (result > 0)
            {

                return getSuccessfulOperation();
            }
            else
            {
                return getFailedOperation();
            }

            //}
            //return View(eng_leave_setting);
        }

        #endregion
        #region Project
        public ActionResult ProjectIndex()
        {
            var project = supplierService.getAllProjects().Where(a => a.IsActive == 1).ToList();
            return View(project);
        }
        public ActionResult ProjectCreate()
        {
          
            //var clients = clientService.getAllClients().Where(a => a.IsActive == 1).Select(a => a.Company_Name).ToList();
            //clients.Insert(0, "-Select-");
            //ViewBag.ClientID = new SelectList(clients);
            //var managers = employeeService.getAllEmployees().Where(a => a.GroupID==2 && a.IsActive == 1).Select(a => a.FullName).ToList();
            //managers.Insert(0, "-Select-");
           /* ViewBag.UserID = new SelectList(managers)*/;
            var clients = clientService.getAllClients().Where(a => a.IsActive == 1).ToList();
            clients.Insert(0, new ClientViewModel() { ClientID = 0, Company_Name = "-Select-" });

            ViewBag.ClientId = new SelectList(clients, "ClientID", "Company_Name");
            var managers = employeeService.getAllEmployees().Where(a => a.GroupID == 2 && a.IsActive == 1).ToList();
            managers.Insert(0, new EmployeeViewModel() { UserID = 0, FullName = "-Select-" });
            ViewBag.UserID = new SelectList(managers, "UserID", "FullName");
            return View();
        }

        // POST: Quote/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ProjectCreate(ProjectNewViewModel eng_project)
        {
            //if (ModelState.IsValid)
            //{
            //db.eng_quote_master.Add(eng_quote_master);
            //db.SaveChanges();
            //return RedirectToAction("Index");\
            if (!string.IsNullOrEmpty(eng_project.StartDate))
                eng_project.StartDate = DateTime.ParseExact(eng_project.StartDate, "dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("dd/MM/yyyy");
            if (!string.IsNullOrEmpty(eng_project.EndDate))
                eng_project.EndDate = DateTime.ParseExact(eng_project.EndDate, "dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("dd/MM/yyyy");

            eng_project.CreatedBy = userServices.getAllUsers().Where(a => a.UserID == AppSession.GetCurrentUserId()).Select(a => a.EmpID).FirstOrDefault(); ;
            eng_project.CreatedDate = DateTime.Now;
            eng_project.IsActive = 1;
            if (eng_project.UserID==0)
            {
                eng_project.UserID = null;
                eng_project.ProjectLeader = null;
            }
            else
            {
                eng_project.ProjectLeader = eng_project.UserID ?? default(int);
            }
            

            var result = supplierService.CreateProject(eng_project);

            if (result > 0)
            {
                return getSuccessfulOperation();
            }
            else
            {
                return getFailedOperation();
            }
        }

        public ActionResult ProjectDelete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            eng_project eng_project = db.eng_project.Find(id);
            if (eng_project == null)
            {
                return HttpNotFound();
            }
            return View(eng_project);
        }

        // POST: Supplier/Delete/5
        [HttpPost, ActionName("ProjectDelete")]
        [ValidateAntiForgeryToken]
        public ActionResult ProjectDeleteConfirmed(int id)
        {

            var result = supplierService.DeleteProject(id);

            if (result > 0)
            {
                return getSuccessfulOperation();
            }
            else
            {
                return getFailedOperation();
            }
        }
        public ActionResult ProjectDetails(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //eng_supplier_master eng_supplier_master = db.eng_supplier_master.Find(id);
            var project = supplierService.getProject(id ?? default(int));
            if (project == null)
            {
                return HttpNotFound();
            }
            
            project.eng_employee_profile2.FullName = project.eng_employee_profile2.FirstName + ' ' + project.eng_employee_profile2.LastName + "(" + project.eng_employee_profile2.EmpID + ")";
            
                
            if(project.eng_employee_profile1!=null)
            {
                project.eng_employee_profile1.FullName = project.eng_employee_profile1.FirstName + ' ' + project.eng_employee_profile1.LastName + "(" + project.eng_employee_profile1.EmpID + ")";

            }
            return View(project);
        }
        public ActionResult ProjectEdit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var project = supplierService.getProject(id ?? default(int));
            var clients = clientService.getAllClients().Where(a => a.IsActive == 1).ToList();

            ViewBag.ClientID = new SelectList(clients, "ClientID", "Company_Name",project.ClientId);
            var managers = employeeService.getAllEmployees().Where(a => a.GroupID == 2 && a.IsActive == 1).ToList();
            if (project.ProjectLeader == null)
            {
                managers.Insert(0, new EmployeeViewModel() { UserID = 0, FullName = "-Select-" });
                ViewBag.UserID = new SelectList(managers, "UserID", "FullName");
            }
            else
            {
                managers.Insert(0, new EmployeeViewModel() { UserID = 0, FullName = "-Select-" });
                ViewBag.UserID = new SelectList(managers, "UserID", "FullName", project.ProjectLeader);
            }
            
            if (project == null)
            {
                return HttpNotFound();
            }

            return View(project);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ProjectEdit(ProjectNewViewModel eng_project)
        {
            //if (ModelState.IsValid)
            //{
            eng_project.UpdatedBy = userServices.getAllUsers().Where(a => a.UserID == AppSession.GetCurrentUserId()).Select(a => a.EmpID).FirstOrDefault(); 
            eng_project.UpdatedDate = DateTime.Now;
            if (eng_project.UserID == 0)
            {
                eng_project.UserID = null;
                eng_project.ProjectLeader = null;
            }
            else
            {
                eng_project.ProjectLeader = eng_project.UserID ?? default(int);
            }
            var result = supplierService.SaveProject(eng_project);
            if (result > 0)
            {

                return getSuccessfulOperation();
            }
            else
            {
                return getFailedOperation();
            }

            //}
            //return View(eng_leave_setting);
        }
        #endregion
        #region Holiday Setting
       
        public ActionResult HolidayIndex()
        {
                return View(supplierService.getAllHoliday().Where(a => a.IsActive == 1).ToList());
        }


        public ActionResult HolidayDelete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            eng_holiday eng_holiday = db.eng_holiday.Find(id);
            if (eng_holiday == null)
            {
                return HttpNotFound();
            }
            return View(eng_holiday);
        }

        // POST: Supplier/Delete/5
        [HttpPost, ActionName("HolidayDelete")]
        [ValidateAntiForgeryToken]
        public ActionResult HolidayDeleteConfirmed(int id)
        {

            var result = supplierService.DeleteHoliday(id);

            if (result > 0)
            {
                return getSuccessfulOperation();
            }
            else
            {
                return getFailedOperation();
            }
        }

        public ActionResult HolidayDetails(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //eng_supplier_master eng_supplier_master = db.eng_supplier_master.Find(id);
            var Holiday = supplierService.getHoliday(id ?? default(int));
            if (Holiday == null)
            {
                return HttpNotFound();
            }
            Holiday.eng_employee_profile.FullName = Holiday.eng_employee_profile.FirstName + ' ' + Holiday.eng_employee_profile.LastName + "(" + Holiday.eng_employee_profile.EmpID + ")";
            if(Holiday.eng_employee_profile1!=null)
            {
                Holiday.eng_employee_profile1.FullName = Holiday.eng_employee_profile1.FirstName + ' ' + Holiday.eng_employee_profile1.LastName + "(" + Holiday.eng_employee_profile1.EmpID + ")";
            }
            
            return View(Holiday);
        }
        public ActionResult HolidayCreate()

        {
            var country_list = supplierService.getAllcountry().ToList();
            country_list.Insert(0, new countryViewModel() { id = 0, name = "-Select-" });
            ViewBag.Country = new SelectList(country_list, "id", "name");
            
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult HolidayCreate(HolidayMasterViewModel eng_holiday)

        {

            //if (ModelState.IsValid)
            //{
            
            if (!string.IsNullOrEmpty(eng_holiday.Date))
                eng_holiday.Date = DateTime.ParseExact(eng_holiday.Date, "dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("dd/MM/yyyy");
            eng_holiday.CreatedBy = userServices.getAllUsers().Where(a => a.UserID == AppSession.GetCurrentUserId()).Select(a => a.EmpID).FirstOrDefault(); 
                eng_holiday.CreatedDate = DateTime.Now;
                eng_holiday.IsActive = 1;

            string sDate = string.Empty;
            int count = 0;
            int Day = 0;
            int Month = 0;
            int Year = 0;
            sDate = eng_holiday.Date;
            string[] Words = sDate.Split(new char[] { '-' });
            foreach (string Word in Words)
            {
                count += 1;
                if (count == 1) { Day = Convert.ToInt16(Word); }
               if  (count == 2) { Month = Convert.ToInt16(Word); }
                if (count == 3) { Year = Convert.ToInt16(Word); }
                
            }

            DateTime dateValue = new DateTime(Year, Month, Day);
            eng_holiday.Day = dateValue.ToString("dddd");
            if (eng_holiday.Country == 0)
            {
                eng_holiday.Country = null;
            }
            var result = supplierService.CreateHoliday(eng_holiday);
                
                if (result > 0)
                {
                    return getSuccessfulOperation();
                }
                else
                {
                    return getFailedOperation();
                }

            //}


            return View(eng_holiday);
        }

        public ActionResult HolidayEdit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //  eng_supplier_master eng_supplier_master = db.eng_supplier_master.Find(id);
            var holiday = supplierService.getHoliday(id ?? default(int));
           
            var country_list = supplierService.getAllcountry().ToList();
            
            if (holiday.Country == null)
            {
                country_list.Insert(0, new countryViewModel() { id = 0, name = "-Select-" });
                ViewBag.Country = new SelectList(country_list, "id", "name");
            }
            else
            {
                country_list.Insert(0, new countryViewModel() { id = 0, name = "-Select-" });
                ViewBag.Country = new SelectList(country_list, "id", "name",holiday.Country);
            }


           
            if (holiday == null)
            {
                return HttpNotFound();
            }

            return View(holiday);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult HolidayEdit(HolidayMasterViewModel eng_holiday)
        {
            //if (ModelState.IsValid)
            //{
            eng_holiday.UpdatedBy = userServices.getAllUsers().Where(a => a.UserID == AppSession.GetCurrentUserId()).Select(a => a.EmpID).FirstOrDefault(); ;
            eng_holiday.UpdatedDate = DateTime.Now;
            string sDate = string.Empty;
            int count = 0;
            int Day = 0;
            int Month = 0;
            int Year = 0;
            sDate = eng_holiday.Date;
            string[] Words = sDate.Split(new char[] { '/' });
            foreach (string Word in Words)
            {
                count += 1;
                if (count == 1) { Day = Convert.ToInt16(Word); }
                if (count == 2) { Month = Convert.ToInt16(Word); }
                if (count == 3) { Year = Convert.ToInt16(Word); }

            }

            DateTime dateValue = new DateTime(Year, Month, Day);
            eng_holiday.Day = dateValue.ToString("dddd");
            if (eng_holiday.Country == 0)
            {
                eng_holiday.Country = null;
            }
            var result = supplierService.SaveHoliday(eng_holiday);
            if (result > 0)
            {

                return getSuccessfulOperation();
            }
            else
            {
                return getFailedOperation();
            }

            //}
            //return View(eng_leave_setting);
        }

        #endregion
        #region Leave Master

        public ActionResult LeaveMasIndex()
        {
            return View(supplierService.getAllLeaveMas().Where(a => a.IsActive == 1).ToList());
        }

        public ActionResult LeaveMasCreate()
        {
           

            var leave = supplierService.getAllLeaveSettings().Where(a => a.IsActive == 1).ToList();
            leave.Insert(0, new LeaveSettingViewModel() { LeaveId = 0, LeaveName = "-Select-" });
            ViewBag.LeaveId = new SelectList(leave, "LeaveId", "LeaveName");
           
            return View();
        }

        // POST: Quote/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LeaveMasCreate(LeaveMasViewModel eng_leave_master)
        {
            //if (ModelState.IsValid)
            //{
            //db.eng_quote_master.Add(eng_quote_master);
            //db.SaveChanges();
            //return RedirectToAction("Index");\
            if (!string.IsNullOrEmpty(eng_leave_master.FromDate))
                eng_leave_master.FromDate = DateTime.ParseExact(eng_leave_master.FromDate, "dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("dd/MM/yyyy");
            if (!string.IsNullOrEmpty(eng_leave_master.ToDate))
                eng_leave_master.ToDate = DateTime.ParseExact(eng_leave_master.ToDate, "dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("dd/MM/yyyy");
            var userid = AppSession.GetCurrentUserId();
            var actualempid = userServices.getAllUsers().Where(a => a.UserID == userid).Select(a => a.EmpID).FirstOrDefault();
            eng_leave_master.CreatedBy = actualempid;
            eng_leave_master.CreatedDate = DateTime.Now;
            eng_leave_master.IsActive = 1;
            eng_leave_master.LeaveMasType = eng_leave_master.LeaveId;
            //eng_leave_master.ProjectLeader = eng_project.UserID;

            var result = supplierService.CreateLeaveMas(eng_leave_master);

            if (result > 0)
            {
                return getSuccessfulOperation();
            }
            else
            {
                return getFailedOperation();
            }
        }

        #endregion
        public ActionResult IndexMenu(int proid)
        {
            var permissions = new List<ProjectPermissionViewModel>();
            if (proid > 0)
            {
                permissions = supplierService.getAllPermissions().Where(a => a.ProjectID == proid).ToList();
            }
            var gname = supplierService.getAllProjects().Where(a => a.IsActive == 1).ToList();
            gname.Insert(0, new ProjectNewViewModel() { ProjectId = 0, ProjectName = "Select" });
            ViewBag.ProjectId = new SelectList(gname, "ProjectId", "ProjectName", proid);

            var module = employeeService.getAllEmployees().Where(a => a.IsActive == 1 && a.GroupID!=1 && a.GroupID!=5).ToList();
            List<SelectListItem> names = new List<SelectListItem>();
            foreach (var cnt in module)
            {
                names.Add(new SelectListItem { Text = cnt.FullName, Value = cnt.UserID.ToString() });
            }

            ViewBag.EmployeeList = names;

            return View(permissions);
        }

        [HttpPost]
        public ActionResult CreateMenu(int projectid, List<int> EmployeeList)
        {
            var result = supplierService.CreateMenu(projectid, EmployeeList);
            if (result > 0)
            {
                return getSuccessfulOperation();
            }
            else
            {
                return getFailedOperation();
            }

        }

        public ActionResult TimeIndex()
        {
            var userid = AppSession.GetCurrentUserId();
            var groupid = AppSession.GetCurrentUserGroup();
            var actualempid = userServices.getAllUsers().Where(a => a.UserID == userid).Select(a => a.EmpID).FirstOrDefault();
            var timeCard = supplierService.getAllTimeEntry(actualempid , groupid).ToList();
            ViewBag.userid = actualempid;



            return View(timeCard);
        }

        public ActionResult CreateTimeEntry()
        {


            var approvers = userServices.getAllUsers().Where(a => a.IsActive == 1 && a.GroupID == 2).ToList();
            ViewBag.ApprovedBy = new SelectList(approvers, "UserID", "DisplayName");

            var project = supplierService.getAllProjects().Where(a => a.IsActive == 1).ToList();
            ViewBag.ProjectID = new SelectList(project, "ProjectID", "ProjectName");
            var leave = supplierService.getAllLeaveSettings().Where(a => a.IsActive == 1).ToList();
            ViewBag.LeaveID = new SelectList(leave, "LeaveID", "LeaveName", "--Select--");

           

           TimeEntrySubmitViewModel timeEntry = new TimeEntrySubmitViewModel();


            return View(timeEntry);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateTimeEntry(TimeEntrySubmitViewModel eng_time_entry_submit)
        {

            //if (ModelState.IsValid)
            //{
                if (!string.IsNullOrEmpty(eng_time_entry_submit.SubmittedDate))
                eng_time_entry_submit.SubmittedDate = DateTime.ParseExact(eng_time_entry_submit.SubmittedDate, "dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("dd/MM/yyyy");

                if (!string.IsNullOrEmpty(eng_time_entry_submit.ApprovedDate))
                eng_time_entry_submit.ApprovedDate = DateTime.ParseExact(eng_time_entry_submit.ApprovedDate, "dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("dd/MM/yyyy");



            if (!string.IsNullOrEmpty(eng_time_entry_submit.WorkStartTime))
                eng_time_entry_submit.WorkStartTime =
                  DateTime.ParseExact(eng_time_entry_submit.WorkStartTime, "yyyy/MM/dd HH:mm", CultureInfo.InvariantCulture).ToString("HH:mm");

            if (!string.IsNullOrEmpty(eng_time_entry_submit.WorkEndTime))
                eng_time_entry_submit.WorkEndTime =
                  DateTime.ParseExact(eng_time_entry_submit.WorkEndTime, "yyyy/MM/dd HH:mm", CultureInfo.InvariantCulture).ToString("HH:mm");

            eng_time_entry_submit.ProjectName = eng_time_entry_submit.ProjectId;
            eng_time_entry_submit.Leave = eng_time_entry_submit.LeaveId;


            eng_time_entry_submit.SubmittedBy = AppSession.GetCurrentUserId();
            eng_time_entry_submit.Status = 0;
            //var User= AppSession.GetCurrentUserId();
            //var TM = userServices.getUser(User ?? default(int));
            eng_time_entry_submit.Employee = AppSession.GetCurrentUserId();
            var result = supplierService.CreateTimeEntrySubmit(eng_time_entry_submit);
                if (result > 0)
                {
                    return getSuccessfulOperation();
                }
                else
                {
                    return getFailedOperation();
                }
            
        }

        public ActionResult EditTimeEntrySubmit(int? id)
        {

            if (id == null)
            {

                return getFailedOperation();
            }

            var TM = supplierService.getTimeEntrySubmit(id ?? default(int));
            if (TM == null)
            {
                return getFailedOperation();
            }
          

            var approvers = userServices.getAllUsers().Where(a => a.IsActive == 1 && a.GroupID == 2).ToList();
            ViewBag.ApprovedBy = new SelectList(approvers, "UserID", "DisplayName",TM.ApprovedBy);

            var project = supplierService.getAllProjects().Where(a => a.IsActive == 1 ).ToList();
            ViewBag.ProjectID = new SelectList(project, "ProjectID", "ProjectName",TM.ProjectName);
            var leave = supplierService.getAllLeaveSettings().Where(a => a.IsActive == 1 ).ToList();
            ViewBag.LeaveID = new SelectList(leave, "LeaveID", "LeaveName",TM.Leave);



            return View(TM);



        }

        // POST: Claim/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult EditTimeEntrySubmit(ClaimViewModel eng_claim)
        //{
        //    if (ModelState.IsValid)
        //    {

        //        foreach (var desc in eng_claim.eng_claim_description)
        //        {
        //            desc.ClaimID = eng_claim.ClaimID;
        //            desc.RecpDate = DateTime.ParseExact(desc.RecpDate, "dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("MM/dd/yyyy");
        //        }


        //        string halfPath = "/images/ClaimFiles/" + AppSession.GetCurrentUserId() + "/";

        //        if (AppSession.GetCurrentUserGroup() == 3)
        //        {
        //            eng_claim.Status = 0;
        //        }
        //        var result = claimService.SaveClaim(eng_claim, Path.Combine(Server.MapPath("~/images/ClaimFiles/" + AppSession.GetCurrentUserId() + "/")), halfPath);
        //        if (result > 0)
        //            return getSuccessfulOperation();
        //        else
        //            return getFailedOperation();
        //    }

        //    return View(eng_claim);
        //}

        [HttpPost, ActionName("ApproveOrRejectTimeEntry")]
        [ValidateAntiForgeryToken]
        public ActionResult ApproveOrRejectTimeEntry(int id, string remarks, int flag)
        {
            var TM = supplierService.getTimeEntrySubmit(id);

            if (flag == 1)
            {
                TM.ApprovalRemarks = remarks;
                TM.ApprovedBy = AppSession.GetCurrentUserId();
                //claim.ApprovedDate = DateTime.Now;
                TM.ApprovedDate = DateTime.Now.ToString("MM/dd/yyyy");
                TM.Status = 1;
              

            }

            if (flag == 2)
            {
                TM.RejectedRemarks = remarks;
                TM.ApprovedBy = AppSession.GetCurrentUserId();
                //claim.ApprovedDate = DateTime.Now;;
                TM.ApprovedDate = DateTime.Now.ToString("MM/dd/yyyy");
                TM.Status = 2;

            }
            var result = supplierService.ApproveRejectTimeEntrySubmit(TM);

            if (result > 0)
            {
                return getSuccessfulOperation();
            }
            else
            {
                return getFailedOperation();
            }


        }

        public ActionResult TimeEntryApprove(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            eng_time_entry_master eng_time_entry_master = db.eng_time_entry_master.Find(id);
            if (eng_time_entry_master == null)
            {
                return HttpNotFound();
            }
            return View(eng_time_entry_master);
        }

        // POST: Supplier/Delete/5
        [HttpPost, ActionName("TimeEntryApprove")]
        [ValidateAntiForgeryToken]
        public ActionResult TimeEntryApproveConfirmed(int id)
        {

            var result = supplierService.ApproveTimeEntry(id);

            if (result > 0)
            {
                return getSuccessfulOperation();
            }
            else
            {
                return getFailedOperation();
            }
        }
        public ActionResult TimeEntryReject(int? id,string remark)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            eng_time_entry_master eng_time_entry_master = db.eng_time_entry_master.Find(id);
            if (eng_time_entry_master == null)
            {
                return HttpNotFound();
            }
            return View(eng_time_entry_master);
        }

        // POST: Supplier/Delete/5
        [HttpPost, ActionName("TimeEntryReject")]
        [ValidateAntiForgeryToken]
        public ActionResult TimeEntryRejectConfirmed(int id, string remark)
        {

            var result = supplierService.RejectTimeEntry(id,remark);

            if (result > 0)
            {
                return getSuccessfulOperation();
            }
            else
            {
                return getFailedOperation();
            }
        }

        public ActionResult TimeEntryDetails(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var TM = supplierService.getTimeEntry(id ?? default(int));

            var tevm = new TimeEntryMasterViewModel();
            DateTime mont = Convert.ToDateTime(TM.Month);
            var tevmnew = supplierService.getTEMaster(TM.EmpID , mont);

            var tegvm = new List<TimeEntryGridViewModel>();

            var PIDs = supplierService.getAllProjectIDs().Where(a => a.EmployeeID == TM.EmpID).ToList();
            var LVs = supplierService.getAllLeaveSettings().Where(a => a.IsActive == 1).ToList();
            if (tevmnew == null)
            {
                foreach (var prj in PIDs)
                {
                    var prjname = supplierService.getProject(prj.ProjectID ?? default(int)).ProjectName;
                    var newTEGVM = new TimeEntryGridViewModel();
                    newTEGVM.ProjectID = prj.ProjectID ?? default(int);
                    newTEGVM.ProjectName = prjname;
                    tegvm.Add(newTEGVM);
                }

                foreach (var lv in LVs)
                {
                    var newTEGVM = new TimeEntryGridViewModel();
                    newTEGVM.ProjectID = 900000 + lv.LeaveId;
                    newTEGVM.ProjectName = lv.LeaveName;
                    tegvm.Add(newTEGVM);
                }

            }
            else
            {
                foreach (var prj in PIDs)
                {
                    var prjname = supplierService.getProject(prj.ProjectID ?? default(int)).ProjectName;
                    var newTEGVM = new TimeEntryGridViewModel();
                    newTEGVM.ProjectID = prj.ProjectID ?? default(int);
                    newTEGVM.ProjectName = prjname;

                    var exTEGVM = tevmnew.tegridvm.Where(a => a.ProjectID == newTEGVM.ProjectID && a.TEMasterID == tevmnew.TEMasterID).FirstOrDefault();
                    if (exTEGVM != null)
                    {
                        newTEGVM.TEGridID = exTEGVM.TEGridID;
                        newTEGVM.TEMasterID = exTEGVM.TEMasterID;

                        newTEGVM.Day1 = exTEGVM.Day1;
                        newTEGVM.Day2 = exTEGVM.Day2;
                        newTEGVM.Day3 = exTEGVM.Day3;
                        newTEGVM.Day4 = exTEGVM.Day4;
                        newTEGVM.Day5 = exTEGVM.Day5;
                        newTEGVM.Day6 = exTEGVM.Day6;
                        newTEGVM.Day7 = exTEGVM.Day7;
                        newTEGVM.Day8 = exTEGVM.Day8;
                        newTEGVM.Day9 = exTEGVM.Day9;
                        newTEGVM.Day10 = exTEGVM.Day10;

                        newTEGVM.Day11 = exTEGVM.Day11;
                        newTEGVM.Day12 = exTEGVM.Day12;
                        newTEGVM.Day13 = exTEGVM.Day13;
                        newTEGVM.Day14 = exTEGVM.Day14;
                        newTEGVM.Day15 = exTEGVM.Day15;
                        newTEGVM.Day16 = exTEGVM.Day16;
                        newTEGVM.Day17 = exTEGVM.Day17;
                        newTEGVM.Day18 = exTEGVM.Day18;
                        newTEGVM.Day19 = exTEGVM.Day19;
                        newTEGVM.Day20 = exTEGVM.Day20;

                        newTEGVM.Day21 = exTEGVM.Day21;
                        newTEGVM.Day22 = exTEGVM.Day22;
                        newTEGVM.Day23 = exTEGVM.Day23;
                        newTEGVM.Day24 = exTEGVM.Day24;
                        newTEGVM.Day25 = exTEGVM.Day25;
                        newTEGVM.Day26 = exTEGVM.Day26;
                        newTEGVM.Day27 = exTEGVM.Day27;
                        newTEGVM.Day28 = exTEGVM.Day28;
                        newTEGVM.Day29 = exTEGVM.Day29;
                        newTEGVM.Day30 = exTEGVM.Day30;
                        newTEGVM.Day31 = exTEGVM.Day31;
                    }

                    tegvm.Add(newTEGVM);
                }

                foreach (var lv in LVs)
                {
                    var newTEGVM = new TimeEntryGridViewModel();
                    newTEGVM.ProjectID = 900000 + lv.LeaveId;
                    newTEGVM.ProjectName = lv.LeaveName;

                    var exTEGVM = tevmnew.tegridvm.Where(a => a.ProjectID == newTEGVM.ProjectID && a.TEMasterID == tevmnew.TEMasterID).FirstOrDefault();
                    if (exTEGVM != null)
                    {
                        newTEGVM.TEGridID = exTEGVM.TEGridID;
                        newTEGVM.TEMasterID = exTEGVM.TEMasterID;

                        newTEGVM.Day1 = exTEGVM.Day1;
                        newTEGVM.Day2 = exTEGVM.Day2;
                        newTEGVM.Day3 = exTEGVM.Day3;
                        newTEGVM.Day4 = exTEGVM.Day4;
                        newTEGVM.Day5 = exTEGVM.Day5;
                        newTEGVM.Day6 = exTEGVM.Day6;
                        newTEGVM.Day7 = exTEGVM.Day7;
                        newTEGVM.Day8 = exTEGVM.Day8;
                        newTEGVM.Day9 = exTEGVM.Day9;
                        newTEGVM.Day10 = exTEGVM.Day10;

                        newTEGVM.Day11 = exTEGVM.Day11;
                        newTEGVM.Day12 = exTEGVM.Day12;
                        newTEGVM.Day13 = exTEGVM.Day13;
                        newTEGVM.Day14 = exTEGVM.Day14;
                        newTEGVM.Day15 = exTEGVM.Day15;
                        newTEGVM.Day16 = exTEGVM.Day16;
                        newTEGVM.Day17 = exTEGVM.Day17;
                        newTEGVM.Day18 = exTEGVM.Day18;
                        newTEGVM.Day19 = exTEGVM.Day19;
                        newTEGVM.Day20 = exTEGVM.Day20;

                        newTEGVM.Day21 = exTEGVM.Day21;
                        newTEGVM.Day22 = exTEGVM.Day22;
                        newTEGVM.Day23 = exTEGVM.Day23;
                        newTEGVM.Day24 = exTEGVM.Day24;
                        newTEGVM.Day25 = exTEGVM.Day25;
                        newTEGVM.Day26 = exTEGVM.Day26;
                        newTEGVM.Day27 = exTEGVM.Day27;
                        newTEGVM.Day28 = exTEGVM.Day28;
                        newTEGVM.Day29 = exTEGVM.Day29;
                        newTEGVM.Day30 = exTEGVM.Day30;
                        newTEGVM.Day31 = exTEGVM.Day31;
                    }

                    tegvm.Add(newTEGVM);
                }


                tevmnew.tegridvm = tegvm;

            }


            var EmpDetails = employeeService.getAllEmployees().Where(a => a.UserID == TM.EmpID).ToList();

         
            tevm.tegridvm = tegvm;



            ViewBag.FirstName = EmpDetails[0].FirstName;
            ViewBag.designation = supplierService.getAllGroups().Where(a => a.GroupID == EmpDetails[0].GroupID).Select(a => a.GroupName).FirstOrDefault();
            ViewBag.rep_manager = EmpDetails[0].ReportManager;

           
            if (tevmnew == null)
            {
                DateTime mont1 = Convert.ToDateTime(tevmnew.Month);
                String mn = mont1.Month.ToString();
                String yy = mont1.Year.ToString();
                ViewBag.month = Convert.ToInt16(mn);
                ViewBag.year = Convert.ToInt16(yy);
                return View(tevm);
            }
            else
            {
                DateTime mont1 = Convert.ToDateTime(tevmnew.Month);
                String mn = mont1.Month.ToString();
                String yy = mont1.Year.ToString();
                ViewBag.month = Convert.ToInt16(mn);
                ViewBag.year = Convert.ToInt16(yy);

                return View(tevmnew);
            }

        }
        public ActionResult TimeEntryEdit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var TM = supplierService.getTimeEntry(id ?? default(int));

            var tevm = new TimeEntryMasterViewModel();
            DateTime mont = Convert.ToDateTime(TM.Month);
            var tevmnew = supplierService.getTEMaster(TM.EmpID, mont);

            var tegvm = new List<TimeEntryGridViewModel>();

            var PIDs = supplierService.getAllProjectIDs().Where(a => a.EmployeeID == TM.EmpID).ToList();
            var LVs = supplierService.getAllLeaveSettings().Where(a => a.IsActive == 1).ToList();
            if (tevmnew == null)
            {
                foreach (var prj in PIDs)
                {
                    var prjname = supplierService.getProject(prj.ProjectID ?? default(int)).ProjectName;
                    var newTEGVM = new TimeEntryGridViewModel();
                    newTEGVM.ProjectID = prj.ProjectID ?? default(int);
                    newTEGVM.ProjectName = prjname;
                    tegvm.Add(newTEGVM);
                }

                foreach (var lv in LVs)
                {
                    var newTEGVM = new TimeEntryGridViewModel();
                    newTEGVM.ProjectID = 900000 + lv.LeaveId;
                    newTEGVM.ProjectName = lv.LeaveName;
                    tegvm.Add(newTEGVM);
                }

            }
            else
            {
                foreach (var prj in PIDs)
                {
                    var prjname = supplierService.getProject(prj.ProjectID ?? default(int)).ProjectName;
                    var newTEGVM = new TimeEntryGridViewModel();
                    newTEGVM.ProjectID = prj.ProjectID ?? default(int);
                    newTEGVM.ProjectName = prjname;

                    var exTEGVM = tevmnew.tegridvm.Where(a => a.ProjectID == newTEGVM.ProjectID && a.TEMasterID == tevmnew.TEMasterID).FirstOrDefault();
                    if (exTEGVM != null)
                    {
                        newTEGVM.TEGridID = exTEGVM.TEGridID;
                        newTEGVM.TEMasterID = exTEGVM.TEMasterID;

                        newTEGVM.Day1 = exTEGVM.Day1;
                        newTEGVM.Day2 = exTEGVM.Day2;
                        newTEGVM.Day3 = exTEGVM.Day3;
                        newTEGVM.Day4 = exTEGVM.Day4;
                        newTEGVM.Day5 = exTEGVM.Day5;
                        newTEGVM.Day6 = exTEGVM.Day6;
                        newTEGVM.Day7 = exTEGVM.Day7;
                        newTEGVM.Day8 = exTEGVM.Day8;
                        newTEGVM.Day9 = exTEGVM.Day9;
                        newTEGVM.Day10 = exTEGVM.Day10;

                        newTEGVM.Day11 = exTEGVM.Day11;
                        newTEGVM.Day12 = exTEGVM.Day12;
                        newTEGVM.Day13 = exTEGVM.Day13;
                        newTEGVM.Day14 = exTEGVM.Day14;
                        newTEGVM.Day15 = exTEGVM.Day15;
                        newTEGVM.Day16 = exTEGVM.Day16;
                        newTEGVM.Day17 = exTEGVM.Day17;
                        newTEGVM.Day18 = exTEGVM.Day18;
                        newTEGVM.Day19 = exTEGVM.Day19;
                        newTEGVM.Day20 = exTEGVM.Day20;

                        newTEGVM.Day21 = exTEGVM.Day21;
                        newTEGVM.Day22 = exTEGVM.Day22;
                        newTEGVM.Day23 = exTEGVM.Day23;
                        newTEGVM.Day24 = exTEGVM.Day24;
                        newTEGVM.Day25 = exTEGVM.Day25;
                        newTEGVM.Day26 = exTEGVM.Day26;
                        newTEGVM.Day27 = exTEGVM.Day27;
                        newTEGVM.Day28 = exTEGVM.Day28;
                        newTEGVM.Day29 = exTEGVM.Day29;
                        newTEGVM.Day30 = exTEGVM.Day30;
                        newTEGVM.Day31 = exTEGVM.Day31;
                    }

                    tegvm.Add(newTEGVM);
                }

                foreach (var lv in LVs)
                {
                    var newTEGVM = new TimeEntryGridViewModel();
                    newTEGVM.ProjectID = 900000 + lv.LeaveId;
                    newTEGVM.ProjectName = lv.LeaveName;

                    var exTEGVM = tevmnew.tegridvm.Where(a => a.ProjectID == newTEGVM.ProjectID && a.TEMasterID == tevmnew.TEMasterID).FirstOrDefault();
                    if (exTEGVM != null)
                    {
                        newTEGVM.TEGridID = exTEGVM.TEGridID;
                        newTEGVM.TEMasterID = exTEGVM.TEMasterID;

                        newTEGVM.Day1 = exTEGVM.Day1;
                        newTEGVM.Day2 = exTEGVM.Day2;
                        newTEGVM.Day3 = exTEGVM.Day3;
                        newTEGVM.Day4 = exTEGVM.Day4;
                        newTEGVM.Day5 = exTEGVM.Day5;
                        newTEGVM.Day6 = exTEGVM.Day6;
                        newTEGVM.Day7 = exTEGVM.Day7;
                        newTEGVM.Day8 = exTEGVM.Day8;
                        newTEGVM.Day9 = exTEGVM.Day9;
                        newTEGVM.Day10 = exTEGVM.Day10;

                        newTEGVM.Day11 = exTEGVM.Day11;
                        newTEGVM.Day12 = exTEGVM.Day12;
                        newTEGVM.Day13 = exTEGVM.Day13;
                        newTEGVM.Day14 = exTEGVM.Day14;
                        newTEGVM.Day15 = exTEGVM.Day15;
                        newTEGVM.Day16 = exTEGVM.Day16;
                        newTEGVM.Day17 = exTEGVM.Day17;
                        newTEGVM.Day18 = exTEGVM.Day18;
                        newTEGVM.Day19 = exTEGVM.Day19;
                        newTEGVM.Day20 = exTEGVM.Day20;

                        newTEGVM.Day21 = exTEGVM.Day21;
                        newTEGVM.Day22 = exTEGVM.Day22;
                        newTEGVM.Day23 = exTEGVM.Day23;
                        newTEGVM.Day24 = exTEGVM.Day24;
                        newTEGVM.Day25 = exTEGVM.Day25;
                        newTEGVM.Day26 = exTEGVM.Day26;
                        newTEGVM.Day27 = exTEGVM.Day27;
                        newTEGVM.Day28 = exTEGVM.Day28;
                        newTEGVM.Day29 = exTEGVM.Day29;
                        newTEGVM.Day30 = exTEGVM.Day30;
                        newTEGVM.Day31 = exTEGVM.Day31;
                    }

                    tegvm.Add(newTEGVM);
                }


                tevmnew.tegridvm = tegvm;

            }


            var EmpDetails = employeeService.getAllEmployees().Where(a => a.UserID == TM.EmpID).ToList();


            tevm.tegridvm = tegvm;



            ViewBag.FirstName = EmpDetails[0].FirstName;
            ViewBag.designation = supplierService.getAllGroups().Where(a => a.GroupID == EmpDetails[0].GroupID).Select(a => a.GroupName).FirstOrDefault();
            ViewBag.rep_manager = EmpDetails[0].ReportManager;


            if (tevmnew == null)
            {
                DateTime mont1 = Convert.ToDateTime(tevmnew.Month);
                String mn = mont1.Month.ToString();
                String yy = mont1.Year.ToString();
                ViewBag.month = Convert.ToInt16(mn);
                ViewBag.year = Convert.ToInt16(yy);
                return View(tevm);
            }
            else
            {
                DateTime mont1 = Convert.ToDateTime(tevmnew.Month);
                String mn = mont1.Month.ToString();
                String yy = mont1.Year.ToString();
                ViewBag.month = Convert.ToInt16(mn);
                ViewBag.year = Convert.ToInt16(yy);

                return View(tevmnew);
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult TimeEntryEdit(TimeEntrySubmitViewModel eng_time_entry_submit)
        {
            //if (ModelState.IsValid)
            //{
            if (!string.IsNullOrEmpty(eng_time_entry_submit.SubmittedDate))
                eng_time_entry_submit.SubmittedDate = DateTime.ParseExact(eng_time_entry_submit.SubmittedDate, "dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("MM/dd/yyyy");

            if (!string.IsNullOrEmpty(eng_time_entry_submit.ApprovedDate))
                eng_time_entry_submit.ApprovedDate = DateTime.ParseExact(eng_time_entry_submit.ApprovedDate, "dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("MM/dd/yyyy");



            if (!string.IsNullOrEmpty(eng_time_entry_submit.WorkStartTime))
                eng_time_entry_submit.WorkStartTime =
                  DateTime.ParseExact(eng_time_entry_submit.WorkStartTime, "HH:mm", CultureInfo.InvariantCulture).ToString("HH:mm");

            if (!string.IsNullOrEmpty(eng_time_entry_submit.WorkEndTime))
                eng_time_entry_submit.WorkEndTime =
                  DateTime.ParseExact(eng_time_entry_submit.WorkEndTime, "HH:mm", CultureInfo.InvariantCulture).ToString("HH:mm");


            eng_time_entry_submit.ProjectName = eng_time_entry_submit.ProjectId;
            eng_time_entry_submit.Leave = eng_time_entry_submit.LeaveId;


            eng_time_entry_submit.SubmittedBy = AppSession.GetCurrentUserId();
            eng_time_entry_submit.Status = 0;
          
           
            var result = supplierService.SaveTimeEntry(eng_time_entry_submit);
            if (result > 0)
            {

                return getSuccessfulOperation();
            }
            else
            {
                return getFailedOperation();
            }

            //}
            //return View(eng_leave_setting);
        }

        public ActionResult IndexTENEW(int? monthnum)
        {
            var empid = AppSession.GetCurrentUserId();
            var actualempid = userServices.getAllUsers().Where(a => a.UserID == empid).Select(a => a.EmpID).FirstOrDefault();
            DateTime now = DateTime.Now;
            var startDate = new DateTime(now.Year, now.Month, 1);
            var endDate = startDate.AddMonths(-1);

            DateTime temonth;
            temonth = new DateTime(now.Year, now.Month, 1);


            if (monthnum == 2)
            {
                temonth = new DateTime(endDate.Year, endDate.Month, 1);
            }

            var tevm = new TimeEntryMasterViewModel();

            var tevmnew = supplierService.getTEMaster(actualempid, temonth);

            var tegvm = new List<TimeEntryGridViewModel>();

            var PIDs = supplierService.getAllProjectIDs().Where(a => a.EmployeeID == actualempid).ToList();
            var LVs = supplierService.getAllLeaveSettings().Where(a => a.IsActive == 1).ToList();
            if (tevmnew == null)
            {
                foreach (var prj in PIDs)
                {
                    var prjname = supplierService.getProject(prj.ProjectID ?? default(int)).ProjectName;
                    var newTEGVM = new TimeEntryGridViewModel();
                    newTEGVM.ProjectID = prj.ProjectID ?? default(int);
                    newTEGVM.ProjectName = prjname;
                    tegvm.Add(newTEGVM);
                }

                foreach (var lv in LVs)
                {
                    var newTEGVM = new TimeEntryGridViewModel();
                    newTEGVM.ProjectID = 900000 + lv.LeaveId;
                    newTEGVM.ProjectName = lv.LeaveName;
                    tegvm.Add(newTEGVM);
                }

            }
            else
            {
                foreach (var prj in PIDs)
                {
                    var prjname = supplierService.getProject(prj.ProjectID ?? default(int)).ProjectName;
                    var newTEGVM = new TimeEntryGridViewModel();
                    newTEGVM.ProjectID = prj.ProjectID ?? default(int);
                    newTEGVM.ProjectName = prjname;

                    var exTEGVM = tevmnew.tegridvm.Where(a => a.ProjectID == newTEGVM.ProjectID && a.TEMasterID == tevmnew.TEMasterID).FirstOrDefault();
                    if (exTEGVM != null)
                    {
                        newTEGVM.TEGridID = exTEGVM.TEGridID;
                        newTEGVM.TEMasterID = exTEGVM.TEMasterID;

                        newTEGVM.Day1 = exTEGVM.Day1;
                        newTEGVM.Day2 = exTEGVM.Day2;
                        newTEGVM.Day3 = exTEGVM.Day3;
                        newTEGVM.Day4 = exTEGVM.Day4;
                        newTEGVM.Day5 = exTEGVM.Day5;
                        newTEGVM.Day6 = exTEGVM.Day6;
                        newTEGVM.Day7 = exTEGVM.Day7;
                        newTEGVM.Day8 = exTEGVM.Day8;
                        newTEGVM.Day9 = exTEGVM.Day9;
                        newTEGVM.Day10 = exTEGVM.Day10;

                        newTEGVM.Day11 = exTEGVM.Day11;
                        newTEGVM.Day12 = exTEGVM.Day12;
                        newTEGVM.Day13 = exTEGVM.Day13;
                        newTEGVM.Day14 = exTEGVM.Day14;
                        newTEGVM.Day15 = exTEGVM.Day15;
                        newTEGVM.Day16 = exTEGVM.Day16;
                        newTEGVM.Day17 = exTEGVM.Day17;
                        newTEGVM.Day18 = exTEGVM.Day18;
                        newTEGVM.Day19 = exTEGVM.Day19;
                        newTEGVM.Day20 = exTEGVM.Day20;

                        newTEGVM.Day21 = exTEGVM.Day21;
                        newTEGVM.Day22 = exTEGVM.Day22;
                        newTEGVM.Day23 = exTEGVM.Day23;
                        newTEGVM.Day24 = exTEGVM.Day24;
                        newTEGVM.Day25 = exTEGVM.Day25;
                        newTEGVM.Day26 = exTEGVM.Day26;
                        newTEGVM.Day27 = exTEGVM.Day27;
                        newTEGVM.Day28 = exTEGVM.Day28;
                        newTEGVM.Day29 = exTEGVM.Day29;
                        newTEGVM.Day30 = exTEGVM.Day30;
                        newTEGVM.Day31 = exTEGVM.Day31;
                    }

                    tegvm.Add(newTEGVM);
                }

                foreach (var lv in LVs)
                {
                    var newTEGVM = new TimeEntryGridViewModel();
                    newTEGVM.ProjectID = 900000 + lv.LeaveId;
                    newTEGVM.ProjectName = lv.LeaveName;

                    var exTEGVM = tevmnew.tegridvm.Where(a => a.ProjectID == newTEGVM.ProjectID && a.TEMasterID == tevmnew.TEMasterID).FirstOrDefault();
                    if (exTEGVM != null)
                    {
                        newTEGVM.TEGridID = exTEGVM.TEGridID;
                        newTEGVM.TEMasterID = exTEGVM.TEMasterID;

                        newTEGVM.Day1 = exTEGVM.Day1;
                        newTEGVM.Day2 = exTEGVM.Day2;
                        newTEGVM.Day3 = exTEGVM.Day3;
                        newTEGVM.Day4 = exTEGVM.Day4;
                        newTEGVM.Day5 = exTEGVM.Day5;
                        newTEGVM.Day6 = exTEGVM.Day6;
                        newTEGVM.Day7 = exTEGVM.Day7;
                        newTEGVM.Day8 = exTEGVM.Day8;
                        newTEGVM.Day9 = exTEGVM.Day9;
                        newTEGVM.Day10 = exTEGVM.Day10;

                        newTEGVM.Day11 = exTEGVM.Day11;
                        newTEGVM.Day12 = exTEGVM.Day12;
                        newTEGVM.Day13 = exTEGVM.Day13;
                        newTEGVM.Day14 = exTEGVM.Day14;
                        newTEGVM.Day15 = exTEGVM.Day15;
                        newTEGVM.Day16 = exTEGVM.Day16;
                        newTEGVM.Day17 = exTEGVM.Day17;
                        newTEGVM.Day18 = exTEGVM.Day18;
                        newTEGVM.Day19 = exTEGVM.Day19;
                        newTEGVM.Day20 = exTEGVM.Day20;

                        newTEGVM.Day21 = exTEGVM.Day21;
                        newTEGVM.Day22 = exTEGVM.Day22;
                        newTEGVM.Day23 = exTEGVM.Day23;
                        newTEGVM.Day24 = exTEGVM.Day24;
                        newTEGVM.Day25 = exTEGVM.Day25;
                        newTEGVM.Day26 = exTEGVM.Day26;
                        newTEGVM.Day27 = exTEGVM.Day27;
                        newTEGVM.Day28 = exTEGVM.Day28;
                        newTEGVM.Day29 = exTEGVM.Day29;
                        newTEGVM.Day30 = exTEGVM.Day30;
                        newTEGVM.Day31 = exTEGVM.Day31;
                    }

                    tegvm.Add(newTEGVM);
                }


                tevmnew.tegridvm = tegvm;

            }


            var EmpDetails = employeeService.getAllEmployees().Where(a => a.UserID == actualempid).ToList();

            tevm.EmpID = actualempid ;
            tevm.ManagerID= EmpDetails[0].Report_Manager ?? default(int);
            tevm.tegridvm = tegvm;
           
           

            ViewBag.FirstName = EmpDetails[0].FirstName +" " + EmpDetails[0].LastName;
            ViewBag.designation = supplierService.getAllGroups().Where(a => a.GroupID == EmpDetails[0].GroupID).Select(a => a.GroupName).FirstOrDefault();
            ViewBag.rep_manager = EmpDetails[0].ReportManager;

            if (monthnum == -1)
            {
                ViewBag.SelMonth = 0;
                tevm = new TimeEntryMasterViewModel();
            }
            else
            {
                ViewBag.SelMonth = monthnum;
            }
           
            if (tevmnew == null || monthnum == -1)
            {
                ViewBag.status =0;
                return View(tevm);
            }
            else
            {
                ViewBag.status = tevmnew.Status;
                return View(tevmnew);
            }


        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SaveDraft(TimeEntryMasterViewModel tevm)
        {
            var EmpDetails = employeeService.getAllEmployees().Where(a => a.UserID == tevm.EmpID).ToList();
            tevm.ManagerID = EmpDetails[0].Report_Manager ?? default(int);
            if (ModelState.IsValid)
            {

                DateTime now = DateTime.Now;
                var startDate = new DateTime(now.Year, now.Month, 1);
                var endDate = startDate.AddMonths(-1);

                var result = 0;
                if (tevm.Month != null)
                {
                    tevm.Month = DateTime.ParseExact(tevm.Month, "dd-MM-yyyy HH:mm:ss", CultureInfo.InvariantCulture).ToString("dd/MM/yyyy");

                }
                else
                {
                    if (tevm.MonthYear == 1)
                    {
                        tevm.Month = new DateTime(now.Year, now.Month, 1).ToString("dd/MM/yyyy");
                    }
                    else if (tevm.MonthYear == 2)
                    {
                        tevm.Month = new DateTime(endDate.Year, endDate.Month, 1).ToString("dd/MM/yyyy");
                    }
                }
                //if (!string.IsNullOrEmpty(tevm.TEMonth))
                //tevm.TEMonth = DateTime.ParseExact(tevm.TEMonth, "MM/dd/yyyy", CultureInfo.InvariantCulture).ToString("MM/dd/yyyy");

                if (tevm.TEMasterID > 0)
                {
                    result = supplierService.SaveDraft(tevm);
                }
                else
                {
                    result = supplierService.Creategrid(tevm);
                }


                if (result > 0)
                {
                    return getSuccessfulOperation();
                }
                else
                {
                    return getFailedOperation();
                }

            }


            return View(tevm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SubmitFinal(TimeEntryMasterViewModel tevm)
        {


            if (ModelState.IsValid)
            {
                if(tevm.ManagerID == 0)
                {
                    TempData["Message"] = "Please Assign Report Manager";
                    
                }
                if(tevm.ReportManager!=null && tevm.ManagerID==0)
                {
                    tevm.ManagerID =( employeeService.getAllEmployees().Where(a => a.UserID == tevm.EmpID).Select(a => a.Report_Manager).SingleOrDefault()) ?? default(int);
                }
                var totalWorkingHour = 0;
                var totalLeaveHour = 0;
                var project = 0;
                foreach (TimeEntryGridViewModel grid in tevm.tegridvm)
                { 
                    if(grid.ProjectID < 90000)
                    {
                        
                        var day = grid.Day1 + grid.Day2 + grid.Day3 + grid.Day4 + grid.Day5 + grid.Day6 + grid.Day7 + grid.Day8 + grid.Day9 + grid.Day10 + grid.Day11 + grid.Day12 + grid.Day13 + grid.Day14 + grid.Day15 + grid.Day16 + grid.Day17 + grid.Day18 + grid.Day19 + grid.Day20 + grid.Day21 + grid.Day22 + grid.Day23 + grid.Day24 + grid.Day25 + grid.Day26 + grid.Day27 + grid.Day28 + grid.Day29 + grid.Day30 + grid.Day31;
                        totalWorkingHour = totalWorkingHour + day ?? default(int);
                        project = project + 1;
                    }
                    else
                    {
                        var day = grid.Day1 + grid.Day2 + grid.Day3 + grid.Day4 + grid.Day5 + grid.Day6 + grid.Day7 + grid.Day8 + grid.Day9 + grid.Day10 + grid.Day11 + grid.Day12 + grid.Day13 + grid.Day14 + grid.Day15 + grid.Day16 + grid.Day17 + grid.Day18 + grid.Day19 + grid.Day20 + grid.Day21 + grid.Day22 + grid.Day23 + grid.Day24 + grid.Day25 + grid.Day26 + grid.Day27 + grid.Day28 + grid.Day29 + grid.Day30 + grid.Day31;
                        totalLeaveHour = totalLeaveHour + day ?? default(int);
                    }

                }
                tevm.TotalWorkingHour = totalWorkingHour;
                tevm.TotalLeaveHour = totalLeaveHour;
                tevm.ProjectCount = project;
                DateTime now = DateTime.Now;
                var startDate = new DateTime(now.Year, now.Month, 1);
                var endDate = startDate.AddMonths(-1);
                

                var result = 0;

                if (tevm.MonthYear == 1)
                {
                    tevm.Month = new DateTime(now.Year, now.Month, 1).ToString("dd/MM/yyyy");
                }
                else if (tevm.MonthYear == 2)
                {
                    tevm.Month = new DateTime(endDate.Year, endDate.Month, 1).ToString("dd/MM/yyyy");
                }
                //if (!string.IsNullOrEmpty(tevm.TEMonth))
                //tevm.TEMonth = DateTime.ParseExact(tevm.TEMonth, "MM/dd/yyyy", CultureInfo.InvariantCulture).ToString("MM/dd/yyyy");
                tevm.Status = 1;
                tevm.SubmittedBy = tevm.EmpID;
                DateTime mont1 = Convert.ToDateTime(DateTime.Now);
                String mn = mont1.Month.ToString();
                String yy = mont1.Year.ToString();
                String dd = mont1.Day.ToString();
                tevm.SubmittedDate= dd + '-' + mn + '-' + yy;
                if (tevm.TEMasterID > 0)
                {
                    result = supplierService.SaveDraft(tevm);
                }
                else
                {
                    result = supplierService.Creategrid(tevm);
                }


                if (result > 0)
                {
                    return getSuccessfulOperation();
                }
                else
                {
                    return getFailedOperation();
                }

            }


            return View(tevm);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SubmitEdit(TimeEntryMasterViewModel tevm)
        {
           
            var totalWorkingHour = 0;
            var totalLeaveHour = 0;
            var project = 0;
            foreach (TimeEntryGridViewModel grid in tevm.tegridvm)
            {
                if (grid.ProjectID < 90000)
                {

                    var day = grid.Day1 + grid.Day2 + grid.Day3 + grid.Day4 + grid.Day5 + grid.Day6 + grid.Day7 + grid.Day8 + grid.Day9 + grid.Day10 + grid.Day11 + grid.Day12 + grid.Day13 + grid.Day14 + grid.Day15 + grid.Day16 + grid.Day17 + grid.Day18 + grid.Day19 + grid.Day20 + grid.Day21 + grid.Day22 + grid.Day23 + grid.Day24 + grid.Day25 + grid.Day26 + grid.Day27 + grid.Day28 + grid.Day29 + grid.Day30 + grid.Day31;
                    totalWorkingHour = totalWorkingHour + day ?? default(int);
                    project = project + 1;
                }
                else
                {
                    var day = grid.Day1 + grid.Day2 + grid.Day3 + grid.Day4 + grid.Day5 + grid.Day6 + grid.Day7 + grid.Day8 + grid.Day9 + grid.Day10 + grid.Day11 + grid.Day12 + grid.Day13 + grid.Day14 + grid.Day15 + grid.Day16 + grid.Day17 + grid.Day18 + grid.Day19 + grid.Day20 + grid.Day21 + grid.Day22 + grid.Day23 + grid.Day24 + grid.Day25 + grid.Day26 + grid.Day27 + grid.Day28 + grid.Day29 + grid.Day30 + grid.Day31;
                    totalLeaveHour = totalLeaveHour + day ?? default(int);
                }

            }
            tevm.TotalWorkingHour = totalWorkingHour;
            tevm.TotalLeaveHour = totalLeaveHour;
            tevm.ProjectCount = project;
            tevm.Status = 1;
            tevm.SubmittedBy = tevm.EmpID;
            DateTime mont1 = Convert.ToDateTime(DateTime.Now);
            String mn = mont1.Month.ToString();
            String yy = mont1.Year.ToString();
            String dd = mont1.Day.ToString();
            tevm.SubmittedDate = dd + '-' + mn + '-' + yy;
            if (ModelState.IsValid)
            {
               
              
                var result = 0;
              
                tevm.Month = DateTime.ParseExact(tevm.Month, "dd-MM-yyyy HH:mm:ss", CultureInfo.InvariantCulture).ToString("dd/MM/yyyy");
                result = supplierService.SaveDraft(tevm);
                //if (tevm.TEMasterID > 0)
                //{
                //    result = supplierService.SaveDraft(tevm);
                //}
                //else
                //{
                //    result = supplierService.Creategrid(tevm);
                //}


                if (result > 0)
                {
                    return getSuccessfulOperation();
                }
                else
                {
                    return getFailedOperation();
                }

            }


            return View(tevm);
        }

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
