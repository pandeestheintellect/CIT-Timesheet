using Eng360Web.Models.Repository.Imp;
using Eng360Web.Models.ViewModel;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Eng360Web.Models.Domain;
using AutoMapper;
using System.Data.Entity;
using Eng360Web.Models.Utility;
using System.Globalization;

namespace Eng360Web.Models.Repository.Interface
{
    public class SupplierRepository : ISupplierRepository
    {
        Eng360Entities1 Eng360DB = new Eng360Entities1();
        Logger logger = LogManager.GetCurrentClassLogger();

        public int CreateSupplier(SupplierViewModel supplier)
        {

            eng_address_master address = new eng_address_master()
            {
                Address1 = supplier.eng_address_master.Address1,
                Address2 = supplier.eng_address_master.Address2,
                City = supplier.eng_address_master.City,
                Fax1 = supplier.eng_address_master.Fax1,
                Email = supplier.eng_address_master.Email,
                Web = supplier.eng_address_master.Web,
                Tel = supplier.eng_address_master.Tel,
                Mobile = supplier.eng_address_master.Mobile,
                Postal_Code = supplier.eng_address_master.Postal_Code
            };
            supplier.AddressID = address.AddressID;

            var _db_supplier = Mapper.Map<eng_supplier_master>(supplier);

            Eng360DB.eng_supplier_master.Add(_db_supplier);
            return Eng360DB.SaveChanges();
        }

        public int SaveSupplier(SupplierViewModel supplier)
        {

            var _db_supplier = Mapper.Map<eng_supplier_master>(supplier);
            Eng360DB.Entry(_db_supplier).State = EntityState.Modified;

            var domainAddress = Eng360DB.eng_address_master.First(a => a.AddressID == supplier.AddressID);

            Eng360DB.Entry(domainAddress).State = EntityState.Modified;

            return Eng360DB.SaveChanges();


        }

        public int DeleteSupplier(int supplierID)
        {


            var _db_supplier = Eng360DB.eng_supplier_master.First(a => a.SupplierID == supplierID);

            _db_supplier.UpdatedBy = AppSession.GetCurrentUserId();
            _db_supplier.UpdatedDate = DateTime.Now;
            _db_supplier.IsActive = 0;


            Eng360DB.Entry(_db_supplier).State = EntityState.Modified;
            return Eng360DB.SaveChanges();
        }

        public SupplierViewModel getSupplier(int supplierID)
        {
            eng_supplier_master eng_supplier_master = Eng360DB.eng_supplier_master.Find(supplierID);

            return Mapper.Map<SupplierViewModel>(eng_supplier_master);
        }

        public List<SupplierViewModel> getAllSuppliers()
        {
            var eng_supplier_master = Eng360DB.eng_supplier_master.ToList();
            var lstSupplierView = Mapper.Map<List<SupplierViewModel>>(eng_supplier_master);
            return lstSupplierView;
        }

        public List<LeaveSettingViewModel> getAllLeaveSettings()
        {
            var eng_lev_master = Eng360DB.eng_leave_settings.ToList();
            var lstSupplierView = Mapper.Map<List<LeaveSettingViewModel>>(eng_lev_master);
            return lstSupplierView;
        }
        public int DeleteLeaveSetting(int leaveID)
        {


            var _db_lev = Eng360DB.eng_leave_settings.First(a => a.LeaveId == leaveID);

            
            _db_lev.IsActive = 0;


            Eng360DB.Entry(_db_lev).State = EntityState.Modified;
            return Eng360DB.SaveChanges();
        }

        public LeaveSettingViewModel getLeaveSetting(int leaveID)
        {
            eng_leave_settings eng_leave_settings = Eng360DB.eng_leave_settings.Find(leaveID);

            return Mapper.Map<LeaveSettingViewModel>(eng_leave_settings);
        }
        public int SaveLeaveSetting(LeaveSettingViewModel leave)
        {

            var _db_leave = Mapper.Map<eng_leave_settings>(leave);
            Eng360DB.Entry(_db_leave).State = EntityState.Modified;



            return Eng360DB.SaveChanges();


        }
        public int CreateLeaveSetting(LeaveSettingViewModel leave)
        {
            var _db_leave = Mapper.Map<eng_leave_settings>(leave);

            Eng360DB.eng_leave_settings.Add(_db_leave);
            return Eng360DB.SaveChanges();
        }
        public List<GroupViewModel> getAllGroups()
        {
            var eng_Group = Eng360DB.eng_usergroup.ToList();
            var lstroleView = Mapper.Map<List<GroupViewModel>>(eng_Group);
            return lstroleView;
        }

        public int DeleteGroup(int GroupID)
        {


            var _db_Group = Eng360DB.eng_usergroup.First(a => a.GroupID == GroupID);

           
            _db_Group.IsActive = 0;


            Eng360DB.Entry(_db_Group).State = EntityState.Modified;
            return Eng360DB.SaveChanges();
        }
        public GroupViewModel getGroup(int GroupId)
        {
            eng_usergroup eng_usergroup = Eng360DB.eng_usergroup.Find(GroupId);

            return Mapper.Map<GroupViewModel>(eng_usergroup);
        }

        public int CreateGroup(GroupViewModel Group)
        {
            var _db_usergroup = Mapper.Map<eng_usergroup>(Group);

            Eng360DB.eng_usergroup.Add(_db_usergroup);
            return Eng360DB.SaveChanges();
        }
        public int SaveGroup(GroupViewModel Group)
        {

            var _db_group = Mapper.Map<eng_usergroup>(Group);
            Eng360DB.Entry(_db_group).State = EntityState.Modified;



            return Eng360DB.SaveChanges();


        }
        public List<ProjectNewViewModel> getAllProjects()
        {
            var eng_project = Eng360DB.eng_project.ToList();
            var lstprojectView = Mapper.Map<List<ProjectNewViewModel>>(eng_project);
            return lstprojectView;
        }

        public int CreateProject(ProjectNewViewModel project)
        {
            var _db_project = Mapper.Map<eng_project>(project);
           
            
            Eng360DB.eng_project.Add(_db_project);
            return Eng360DB.SaveChanges();
        }
        public int DeleteProject(int projectID)
        {


            var _db_Project = Eng360DB.eng_project.First(a => a.ProjectId == projectID);

            
          
            _db_Project.IsActive = 0;


            Eng360DB.Entry(_db_Project).State = EntityState.Modified;
            return Eng360DB.SaveChanges();
        }
        public ProjectNewViewModel getProject(int ProjectId)
        {
            eng_project eng_project = Eng360DB.eng_project.Find(ProjectId);

            return Mapper.Map<ProjectNewViewModel>(eng_project);
        }
      
        
        public int SaveProject(ProjectNewViewModel project)
        {

            var _db_project = Mapper.Map<eng_project>(project);
            Eng360DB.Entry(_db_project).State = EntityState.Modified;
            


            return Eng360DB.SaveChanges();


        }

        public List<HolidayMasterViewModel> getAllHoliday()
        {
            var eng_holiday = Eng360DB.eng_holiday.ToList();
            var lstholidayView = Mapper.Map<List<HolidayMasterViewModel>>(eng_holiday);
            return lstholidayView;
        }

        public int DeleteHoliday(int holidayID)
        {


            var _db_Holiday = Eng360DB.eng_holiday.First(a => a.HolidayId == holidayID);

          
            _db_Holiday.IsActive = 0;


            Eng360DB.Entry(_db_Holiday).State = EntityState.Modified;
            return Eng360DB.SaveChanges();
        }
        public HolidayMasterViewModel getHoliday(int HolidayId)
        {
            eng_holiday eng_holiday = Eng360DB.eng_holiday.Find(HolidayId);

            return Mapper.Map<HolidayMasterViewModel>(eng_holiday);
        }

        public int CreateHoliday(HolidayMasterViewModel holiday)
        {
            var _db_holiday = Mapper.Map<eng_holiday>(holiday);

            Eng360DB.eng_holiday.Add(_db_holiday);
            return Eng360DB.SaveChanges();
        }
        public int SaveHoliday(HolidayMasterViewModel Holiday)
        {

            var _db_holiday = Mapper.Map<eng_holiday>(Holiday);
            Eng360DB.Entry(_db_holiday).State = EntityState.Modified;



            return Eng360DB.SaveChanges();


        }

        public List<ProjectPermissionViewModel> getAllPermissions()
        {
            var Permn = Eng360DB.eng_project_permission.ToList();
            var result = Mapper.Map<List<ProjectPermissionViewModel>>(Permn);
            return result;
        }
        public List<LeaveMasViewModel> getAllLeaveMas()
        {
            var eng_leave_master = Eng360DB.eng_leave_master.ToList();
            var lstleaveView = Mapper.Map<List<LeaveMasViewModel>>(eng_leave_master);
            return lstleaveView;
        }
        public int CreateLeaveMas(LeaveMasViewModel leave)
        {
            var _db_leave = Mapper.Map<eng_leave_master>(leave);

            Eng360DB.eng_leave_master.Add(_db_leave);
            return Eng360DB.SaveChanges();
        }
        public int CreateMenu(int projectid, List<int> employeeNames)
        {
            var modules = Eng360DB.eng_project_permission.Where(a => a.ProjectID == projectid).ToList();

            foreach (var mods in modules)
            {
                var delmod = Eng360DB.eng_project_permission.First(a => a.Project_Permission_ID == mods.Project_Permission_ID);
                Eng360DB.eng_project_permission.Remove(delmod);
            }
            if (employeeNames!=null)
            {
                foreach (var mod in employeeNames)
                {
                    eng_project_permission domainPer = new eng_project_permission();
                    domainPer.ProjectID = projectid;
                    domainPer.EmployeeID = mod;
                    domainPer.Access = 1;
                    Eng360DB.eng_project_permission.Add(domainPer);
                }
            }
           
            return Eng360DB.SaveChanges();
        }
        public List<TimeEntryMasterViewModel> getAllTimeEntry(int userid, int groupid)
        {
            List<eng_time_entry_master> timeEntryList;

            if (groupid == 2)
            {
                //claimList = Eng360DB.eng_claim.Where(a => a.ApprovedBy == userid).ToList();

                timeEntryList = Eng360DB.eng_time_entry_master.Where(a => a.ManagerID == userid || a.EmpID == userid).ToList();
            }
            else
            {
                timeEntryList = Eng360DB.eng_time_entry_master.Where(a => a.SubmittedBy == userid).ToList();
            }


            var lstTimeEntryView = Mapper.Map<List<TimeEntryMasterViewModel>>(timeEntryList);
            
            return lstTimeEntryView;
        }
        public List<TimeEntryMasterViewModel> getAllTimeEntries()
        {
            var eng_time_entry_master = Eng360DB.eng_time_entry_master.ToList();
            var lstleaveView = Mapper.Map<List<TimeEntryMasterViewModel>>(eng_time_entry_master);
            return lstleaveView;
        }
        public int CreateTimeEntrySubmit(TimeEntrySubmitViewModel eng_time)
        {
           
                eng_time_entry_submit domainClaim = Mapper.Map<eng_time_entry_submit>(eng_time);
                Eng360DB.eng_time_entry_submit.Add(domainClaim);

                var result = Eng360DB.SaveChanges();


              
                        Eng360DB.SaveChanges();
                        return result;
    
         
        }

        public TimeEntrySubmitViewModel getTimeEntrySubmit(int TimeEntryId)
        {
            eng_time_entry_submit eng_time_entry_submit = Eng360DB.eng_time_entry_submit.AsNoTracking().Where(a => a.TimeEntryId == TimeEntryId).FirstOrDefault();

            return Mapper.Map<TimeEntrySubmitViewModel>(eng_time_entry_submit);
        }
        public int ApproveRejectTimeEntrySubmit(TimeEntrySubmitViewModel TM)
        {

            //try
            //{
                eng_time_entry_submit eng_time_entry_submit = Eng360DB.eng_time_entry_submit.AsNoTracking().Where(a => a.TimeEntryId == TM.TimeEntryId).FirstOrDefault();

                eng_time_entry_submit.ApprovalRemarks = TM.ApprovalRemarks;
                eng_time_entry_submit.ApprovedBy = TM.ApprovedBy;
                eng_time_entry_submit.ApprovedDate = DateTime.Now;
                eng_time_entry_submit.Status = TM.Status;
               
                eng_time_entry_submit.RejectedRemarks = TM.RejectedRemarks;

                Eng360DB.Entry(eng_time_entry_submit).State = EntityState.Modified;

                return Eng360DB.SaveChanges();

            
            //catch (Exception ex)
            //{
            //    logger.Info("Claim Approval Error:");
            //    logger.Error(ex.Message);
            //    logger.Error(ex.StackTrace);


            //    return -1;
            //}

            // return Eng360DB.SaveChanges();

        }

        public int ApproveTimeEntry(int TM)
        {


            var _db_TM = Eng360DB.eng_time_entry_master.First(a => a.TEMasterID == TM);

            _db_TM.ApprovedBy = AppSession.GetCurrentUserId();
            _db_TM.ApprovalDate = DateTime.Now;
            _db_TM.status = 2;


            Eng360DB.Entry(_db_TM).State = EntityState.Modified;
            return Eng360DB.SaveChanges();
        }
        public int RejectTimeEntry(int TM, string remark)
        {

            
            var _db_TM = Eng360DB.eng_time_entry_master.First(a => a.TEMasterID == TM);

            _db_TM.RejectedBy= AppSession.GetCurrentUserId();
            _db_TM.RejectedDate = DateTime.Now;
            _db_TM.RejectedRemarks = remark;
            _db_TM.status = 3;
           

            Eng360DB.Entry(_db_TM).State = EntityState.Modified;
            return Eng360DB.SaveChanges();
        }

        public List<countryViewModel> getAllcountry()
        {
            var Permn = Eng360DB.countryMasters.ToList();
            var result = Mapper.Map<List<countryViewModel>>(Permn);
            return result;
        }
        public TimeEntryMasterViewModel getTimeEntry(int id)
        {
            eng_time_entry_master eng_time_entry_master = Eng360DB.eng_time_entry_master.Find(id);

            return Mapper.Map<TimeEntryMasterViewModel>(eng_time_entry_master);
        }
        public int SaveTimeEntry(TimeEntrySubmitViewModel tm)
        {

            var _db_holiday = Mapper.Map<eng_time_entry_submit>(tm);
            Eng360DB.Entry(_db_holiday).State = EntityState.Modified;



            return Eng360DB.SaveChanges();


        }


        public List<PEMappingViewModel> getAllProjectIDs()
        {

            var pemap = Eng360DB.eng_project_permission.ToList();
            var lstPEMap = Mapper.Map<List<PEMappingViewModel>>(pemap);
            return lstPEMap;


        }
        public TimeEntryMasterViewModel getTEMaster(int EID, DateTime temonth)
        {
            var tem = Eng360DB.eng_time_entry_master.Where(a => a.EmpID == EID && a.Month == temonth).FirstOrDefault();

            var temnew = Mapper.Map<TimeEntryMasterViewModel>(tem);
            if (temnew != null)
            {
                var teg = Eng360DB.eng_time_entry_grid.Where(a => a.TEMasterID == tem.TEMasterID).ToList();

                temnew.tegridvm = Mapper.Map<List<TimeEntryGridViewModel>>(teg);
            }

            return temnew;
        }

        public int SaveDraft(TimeEntryMasterViewModel tevm)
        {

            //try
            //{
                if (!string.IsNullOrEmpty(tevm.Month))
                    tevm.Month = DateTime.ParseExact(tevm.Month, "dd-MM-yyyy", CultureInfo.InvariantCulture).ToString("dd/MM/yyyy");

                eng_time_entry_master _db_tevm = Mapper.Map<eng_time_entry_master>(tevm);

           
            Eng360DB.Entry(_db_tevm).State = EntityState.Modified;

                foreach (var desc in tevm.tegridvm)
                {
                    if (desc.TEGridID > 0)
                    {
                        eng_time_entry_grid teDetail = Mapper.Map<eng_time_entry_grid>(desc);
                        //var teDetail = Eng360DB.eng_time_entry_grid.Find(desc.TEGridID);
                        teDetail.TEMasterID = tevm.TEMasterID;
                        Eng360DB.Entry(teDetail).State = EntityState.Modified;
                    }
                    else
                    {
                        eng_time_entry_grid teDetail = Mapper.Map<eng_time_entry_grid>(desc);
                        teDetail.TEMasterID = tevm.TEMasterID;
                        Eng360DB.eng_time_entry_grid.Add(teDetail);
                    }

                }

                return Eng360DB.SaveChanges();

            //}
            //catch (Exception ex)
            //{
            //    logger.Debug("TE Save:");
            //    logger.Debug(ex.Message);
            //    logger.Debug(ex.StackTrace);
            //    if (ex.InnerException.Message != null)
            //    {
            //        logger.Debug(ex.InnerException.Message);
            //        logger.Debug(ex.InnerException.StackTrace);
            //    }
            //    return -1;
            //}


        }

        public int Creategrid(TimeEntryMasterViewModel tevm)
        {
            try
            {
                if (!string.IsNullOrEmpty(tevm.Month))
                    tevm.Month = DateTime.ParseExact(tevm.Month, "dd-MM-yyyy", CultureInfo.InvariantCulture).ToString("dd/MM/yyyy");

                eng_time_entry_master _db_tevm = Mapper.Map<eng_time_entry_master>(tevm);

                Eng360DB.eng_time_entry_master.Add(_db_tevm);
                Eng360DB.SaveChanges();
                foreach (var desc in tevm.tegridvm)
                {
                    eng_time_entry_grid teDetail = Mapper.Map<eng_time_entry_grid>(desc);
                    teDetail.TEMasterID = _db_tevm.TEMasterID;
                    Eng360DB.eng_time_entry_grid.Add(teDetail);
                }

                return Eng360DB.SaveChanges();

            }
            catch (Exception ex)
            {
                logger.Debug("TE Submission:");
                logger.Debug(ex.Message);
                logger.Debug(ex.StackTrace);
                if (ex.InnerException.Message != null)
                {
                    logger.Debug(ex.InnerException.Message);
                    logger.Debug(ex.InnerException.StackTrace);
                }
                return -1;
            }
        }

    }
}