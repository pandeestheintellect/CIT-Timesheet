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
using Eng360Web.Models.Security;

namespace Eng360Web.Controllers
{
    [AccessDeniedAuthorize]
    public class UserController : SuperBaseController
    {
        private Eng360Entities1 db = new Eng360Entities1();

        private readonly IUserServices userService;
        private readonly ISupplierServices supplierService;
        private readonly IEmployeeServices employeeService;
        
        public UserController(IUserServices _userService, ISupplierServices _supplierService, IEmployeeServices _employeeServices)
        {
            userService = _userService;
            employeeService = _employeeServices;
            supplierService = _supplierService;
        }

        // GET: User
        public ActionResult Index()
        {
            return View(userService.getAllUsers().Where(a=>a.GroupID != 1 && a.IsActive != 0).ToList());
        }

        // GET: User
        //public ActionResult IndexMenu()
        //{
        //    var permissions = userService.getAllPermissions().ToList();            
        //    return View(permissions);
        //}



        // GET: User
        public ActionResult IndexMenu(int grpid)
        {
            var permissions = new List<PermissionViewModel>();
            if (grpid > 0)
            {
                permissions = userService.getAllPermissions().Where(a => a.GroupID == grpid).ToList();
            }
            var gname = supplierService.getAllGroups().Where(a => a.IsActive == 1).ToList();
            gname.Insert(0, new GroupViewModel() { GroupID = 0, GroupName = "Select" });
            ViewBag.GroupID = new SelectList(gname, "GroupID", "GroupName", grpid);

            var module = userService.GetAllModules().OrderBy(a => a.ModuleID).ToList();
            List<SelectListItem> names = new List<SelectListItem>();
            foreach (var cnt in module)
            {
                names.Add(new SelectListItem { Text = cnt.ModuleName, Value = cnt.ModuleID.ToString() });
            }

            ViewBag.ModuleList = names;

            return View(permissions);
        }


        [HttpPost]
        public ActionResult CreateMenu(int groupid, List<int> moduleList)
        {
            var result = userService.CreateMenu(groupid, moduleList);
            if (result > 0)
            {
                return getSuccessfulOperation();
            }
            else
            {
                return getFailedOperation();
            }

        }



        // GET: User/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            // eng_users eng_users = db.eng_users.Find(id);
            var user = userService.getUser(id ?? default(int));
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // GET: User/Create
        //public ActionResult CreateMenu()
        //{
        //    var gname = userService.getUsergroups().ToList();
        //    gname.Insert(0, new GroupViewModel() { GroupID = 0, GroupName = "Select" });
        //    ViewBag.GroupID = new SelectList(gname, "GroupID", "GroupName");
        //    var module = userService.GetAllModules().ToList();
        //    module.Insert(0, new ModuleViewModel() { ModuleID = 0, ModuleName = "Select" });
        //    ViewBag.ModuleID = new SelectList(module, "ModuleID", "ModuleName");                        
        //    return View();
        //}

        // POST: User/Create
        
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult CreateMenu(PermissionViewModel eng_perm)
        //{
        //    if (ModelState.IsValid)
        //    {
        //           eng_perm.Access = 1;

        //            var result = userService.CreateMenu(eng_perm);
        //            if (result > 0)
        //            {
        //                return getSuccessfulOperation();
        //            }
        //            else if (result < 0)
        //            {
        //                return getFailedOperation("Menu Access already available" );
        //            }    
        //            else
        //            {
        //            return getFailedOperation();
        //            }            

        //    }

        //    return View(eng_perm);
        //}

        // GET: User/Create
        public ActionResult Create(int? emp)
        {
            var fnames = userService.getEmployees().Where(a => a.IsActive == 1).ToList();
            fnames.Insert(0, new EmployeeViewModel() { UserID = 0, FullName = "-Select-" });
            ViewBag.EmpID = new SelectList(fnames, "UserID", "FullName");
            var empids = userService.getAllUsers().Where(a => a.IsActive == 1).Select(a => a.EmpID).ToList();
            var newlist = employeeService.getAllEmployees().Where(a => a.IsActive == 1).ToList();
            var count = newlist.Count();
            foreach (int id in empids)
            {
               
                var fn = employeeService.getAllEmployees().Where(a => a.IsActive == 1 && a.UserID != id).ToList();
                
                if (count != fn.Count())
                {
                    var newfn = employeeService.getAllEmployees().Where(a => a.IsActive == 1 && a.UserID != id).ToList();
                    count =count-1;
                }
            }
            
           
            ViewBag.GroupID = new SelectList(userService.getUsergroups().Where(a=>a.GroupID != 1 && a.IsActive == 1).ToList(), "GroupID", "GroupName");
            //ViewBag.EmpID = new SelectList(userService.getEmployees().Where(a => a.IsActive == 1).ToList(), "UserID", "FullName");
            
            return View();
        }
        //public ActionResult GetDetails(int? emp)
        //{
           
        //    var fn = employeeService.getAllEmployees().Where(a => a.IsActive == 1).ToList();
        //    fn.Insert(0, new EmployeeViewModel() { UserID = 0, FullName = "-Select-" });
        //    ViewBag.EmpID = new SelectList(fn, "UserID", "FullName");
        //    ViewBag.GroupID = new SelectList(userService.getUsergroups().Where(a => a.GroupID != 1 && a.IsActive == 1).ToList(), "GroupID", "GroupName");
           
        //    ViewBag.user = "empty";
        //    return null;
        //}


        // POST: User/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(UserViewModel eng_users)
        {
            //if (ModelState.IsValid)
            //{
                //db.eng_users.Add(eng_users);
                //db.SaveChanges();

                if (userService.chkUser(eng_users.UserName) == false)
                {
                eng_users.CreatedBy = userService.getAllUsers().Where(a => a.UserID == AppSession.GetCurrentUserId()).Select(a => a.EmpID).FirstOrDefault();
                eng_users.CreatedDate = DateTime.Now;
                    eng_users.IsActive = 1;
                    var emp = eng_users.EmpID;
                    eng_users.GroupID = employeeService.getAllEmployees().Where(a => a.UserID == emp).Select(a => a.GroupID).FirstOrDefault();
                    var result = userService.CreateUser(eng_users);
                    if (result > 0)
                    {
                        return getSuccessfulOperation();
                    }
                    else
                    {
                        return getFailedOperation();
                    }
                }
                else
                {
                    return getFailedOperation("User name " + eng_users.UserName + " already exist");
                }

            //}

            return View(eng_users);
        }

        // GET: User/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //   eng_users eng_users = db.eng_users.Find(id);
            var user = userService.getUser(id ?? default(int));
            if (user == null)
            {
                return HttpNotFound();
            }
            ViewBag.GroupID = new SelectList(userService.getUsergroups().Where(a => a.GroupID != 1).ToList(), "GroupID", "GroupName", user.GroupID);
            ViewBag.FirstName = employeeService.getAllEmployees().Where(a=>a.UserID==user.EmpID).Select(a=>a.FullName).SingleOrDefault();

            return View(user);
        }

        // POST: User/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(UserViewModel eng_users)
        {

            eng_users.UpdatedBy = userService.getAllUsers().Where(a => a.UserID == AppSession.GetCurrentUserId()).Select(a => a.EmpID).FirstOrDefault(); ;
            eng_users.UpdatedDate = DateTime.Now;

            var result = userService.SaveUser(eng_users);

                if (result > 0)
                {

                    return getSuccessfulOperation();
                }
                else
                {
                    return getFailedOperation();
                }

                //db.Entry(eng_users).State = EntityState.Modified;
                //db.SaveChanges();


            
            return View(eng_users);
        }

        // GET: User/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            eng_users eng_users = db.eng_users.Find(id);
            if (eng_users == null)
            {
                return HttpNotFound();
            }
            return View(eng_users);
        }

        // POST: User/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {

            var result = userService.DeleteUser(id);

            if (result > 0)
            {
                return getSuccessfulOperation();
            }
            else
            {
                return getFailedOperation();
            }
           
        }

        // POST: User/Delete/5
        //[HttpPost, ActionName("DeleteMenu")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteMenuConfirmed(int id)
        //{

        //    var result = userService.DeleteMenu(id);

        //    if (result > 0)
        //    {
        //        return getSuccessfulOperation();
        //    }
        //    else
        //    {
        //        return getFailedOperation();
        //    }

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
