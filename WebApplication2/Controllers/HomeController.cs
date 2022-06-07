using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Eng360Web.Models.Security;
using Eng360Web.Models.Service.Imp;
using Eng360Web.Models.Service.Interface;
using Eng360Web.Models.Utility;
using Eng360Web.Models.ViewModel;

namespace Eng360Web.Controllers
{
    [AccessDeniedAuthorize]
    public class HomeController : Controller
    {
        private readonly IUserServices userService;
        private readonly IEmployeeServices employeeService;
        private readonly IQuoteServices quoteService;
        private readonly ISupplierServices supplierService;
        private readonly IClientServices clientService;



        public HomeController(UserServices _userService, IEmployeeServices _employeeService, IQuoteServices _quoteService,ISupplierServices _supplierService, IClientServices _clientService)
        {
            userService = _userService;
            employeeService = _employeeService;
            quoteService = _quoteService;
            supplierService = _supplierService;
            clientService = _clientService;
        }
        public ActionResult Index()
        {
            var groupid = Models.Utility.AppSession.GetCurrentUserGroup();
            var userid = Models.Utility.AppSession.GetCurrentUserId();


            var Ivm = new IndexViewModel();

            if (groupid == 1 ||  groupid == 5)
            {
                var HrDbd = employeeService.getHREmpDashBoardResults().ToList();
                Ivm.DB_Emp = HrDbd;

                var CpyDbd = employeeService.getCompanyDashBoardResults().ToList();
                Ivm.DB_Company = CpyDbd;

                var VehDbd = employeeService.getVehicleDashBoardResults().ToList();
                Ivm.DB_Vehicle = VehDbd;
            }

            if (groupid == 1 || groupid == 4 || groupid == 5)
            {
                var PayDbd = employeeService.getPayableDashBoardResults().ToList();
                Ivm.DB_Payable = PayDbd;

                var RecDbd = employeeService.getReceivableDashBoardResults().ToList();
                Ivm.DB_Receivable = RecDbd;

            }

            if (groupid == 1 || groupid == 2 || groupid == 3 || groupid == 5)
            {
                var OpDbd = employeeService.getOperationDashBoardResults(groupid, userid).ToList();
                Ivm.DB_Operation = OpDbd;
            }

            if (groupid == 4)
            {
                var DirProjDbd = employeeService.getDirectorProjectDashBoardResults().ToList();
                Ivm.DB_Director_Project = DirProjDbd;
            }

            if (groupid == 2 || groupid == 3)
            {
                var projDbd = employeeService.getSVManProjectDashBoardResults(groupid, userid).ToList();
                Ivm.DB_SVMan_Project = projDbd;

                var dttrDbd = employeeService.getSVManDTTRDashBoardResults(groupid, userid).ToList();
                Ivm.DB_SVMan_DTTR = dttrDbd;

                var qlyDbd = employeeService.getSVManQualityDashBoardResults(groupid, userid).ToList();
                Ivm.DB_SVMan_Quality = qlyDbd;

                var HrDbd = employeeService.getHREmpDashBoardResults().ToList();
                Ivm.DB_Emp = HrDbd;
            }
            
            var actualempid = userService.getAllUsers().Where(a => a.UserID == userid).Select(a => a.EmpID).FirstOrDefault();

            var projectcount = supplierService.getAllProjects().Where(a => a.IsActive == 1).ToList();
            Ivm.ProjectCount = projectcount.Count;
            var client_count = clientService.getAllClients().Where(a => a.IsActive == 1).ToList();
            Ivm.ClientCount = client_count.Count;
            var empCount = employeeService.getAllEmployees().Where(a => a.IsActive == 1 ).ToList();
            Ivm.EmpCount = empCount.Count;
            var userCount = userService.getAllUsers().Where(a => a.IsActive == 1 && a.UserName!="admin").ToList();
            Ivm.UserCount = userCount.Count ;
            var managerempcount = employeeService.getAllEmployees().Where(a => a.IsActive == 1 && a.Report_Manager == actualempid).ToList();
            Ivm.ManagerEmpCount = managerempcount.Count;
            var pending=supplierService.getAllTimeEntries().Where(a => (a.Status == 1 && a.ManagerID==actualempid) || (a.Status == 1 && a.SubmittedBy ==actualempid)).ToList();
            var Approved = supplierService.getAllTimeEntries().Where(a => (a.Status == 2 && a.ManagerID == actualempid) || (a.Status == 2&& a.SubmittedBy == actualempid )).ToList();
            var Rejected = supplierService.getAllTimeEntries().Where(a => (a.Status == 3 && a.ManagerID == actualempid )|| (a.Status == 3 && a.SubmittedBy == actualempid )).ToList();
            Ivm.PendingCount = pending.Count;
            Ivm.ApprovedCount = Approved.Count;
            Ivm.RejectedCount = Rejected.Count;
            //var annualLeave =supplierService.getAllTimeEntries().
            return View(Ivm);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }
        public ActionResult About1()
        {
            ViewBag.Message = "Your application description page.";

            return Json(new
            {
                value = string.Format("{0}", "OK")
            },
                JsonRequestBehavior.AllowGet);
        }
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public ActionResult LeftMenu()
        {
         List< ModuleViewModel> lstModules=   userService.GetModuleNames(AppSession.GetCurrentUserId());
            if (lstModules != null)
            {

                lstModules= lstModules.OrderBy(o => o.order_by).ToList();
            }
            return PartialView(lstModules);
        }
    }
}