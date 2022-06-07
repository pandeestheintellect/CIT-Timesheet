using Eng360Web.Models.Repository.Imp;
using Eng360Web.Models.Service.Imp;
using Eng360Web.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Eng360Web.Models.Service.Interface
{
    public class SupplierServices : ISupplierServices
    {
        private readonly ISupplierRepository supplierRepository;
        public SupplierServices(ISupplierRepository _supplierRepository)
        {
            supplierRepository = _supplierRepository;
        }


        public int CreateSupplier(SupplierViewModel supplier)
        {
            return supplierRepository.CreateSupplier(supplier);
        }

        public int SaveSupplier(SupplierViewModel supplier)
        {
            return supplierRepository.SaveSupplier(supplier);
        }
        public int DeleteSupplier(int supplierID)
        {
            return supplierRepository.DeleteSupplier(supplierID);
        }

        public SupplierViewModel getSupplier(int supplierID)
        {

            return supplierRepository.getSupplier(supplierID);
        }
        public List<SupplierViewModel> getAllSuppliers()
        {
            return supplierRepository.getAllSuppliers();
        }
        public List<LeaveSettingViewModel> getAllLeaveSettings()
        {
            return supplierRepository.getAllLeaveSettings();

        }
        public int DeleteLeaveSetting(int leaveID)
        {
            return supplierRepository.DeleteLeaveSetting(leaveID);
        }



        public LeaveSettingViewModel getLeaveSetting(int leaveID)
        {

            return supplierRepository.getLeaveSetting(leaveID);
        }

        public int SaveLeaveSetting(LeaveSettingViewModel leave)
        {
            return supplierRepository.SaveLeaveSetting(leave);
        }


        public int CreateLeaveSetting(LeaveSettingViewModel leave)
        {
            return supplierRepository.CreateLeaveSetting(leave);
        }
        public List<GroupViewModel> getAllGroups()
        {
            return supplierRepository.getAllGroups();
        }


        public int DeleteGroup(int GroupID)
        {
            return supplierRepository.DeleteGroup(GroupID);
        }
        public GroupViewModel getGroup(int GroupId)
        {

            return supplierRepository.getGroup(GroupId);
        }
        public int CreateGroup(GroupViewModel Group)
        {
            return supplierRepository.CreateGroup(Group);
        }
        public int SaveGroup(GroupViewModel Group)
        {
            return supplierRepository.SaveGroup(Group);
        }
        public List<ProjectNewViewModel> getAllProjects()
        {
            return supplierRepository.getAllProjects();
        }
        public List<PEMappingViewModel> getAllProjectIDs()
        {
            return supplierRepository.getAllProjectIDs();
        }
        public int CreateProject(ProjectNewViewModel project)
        {
            return supplierRepository.CreateProject(project);
        }
        public int DeleteProject(int ProjectID)
        {
            return supplierRepository.DeleteProject(ProjectID);
        }
        public ProjectNewViewModel getProject(int ProjectId)
        {

            return supplierRepository.getProject(ProjectId);
        }
        public int SaveProject(ProjectNewViewModel project)
        {
            return supplierRepository.SaveProject(project);
        }
        public List<HolidayMasterViewModel> getAllHoliday()
        {
            return supplierRepository.getAllHoliday();
        }
        public int CreateTimeEntrySubmit(TimeEntrySubmitViewModel eng_time)
        {
            return supplierRepository.CreateTimeEntrySubmit(eng_time);
        }

        public int DeleteHoliday(int HolidayID)
        {
            return supplierRepository.DeleteHoliday(HolidayID);
        }
        public HolidayMasterViewModel getHoliday(int HolidayID)
        {

            return supplierRepository.getHoliday(HolidayID);
        }
        public int CreateHoliday(HolidayMasterViewModel holiday)
        {
            return supplierRepository.CreateHoliday(holiday);
        }
        public int SaveHoliday(HolidayMasterViewModel holiday)
        {
            return supplierRepository.SaveHoliday(holiday);
        }
        public List<LeaveMasViewModel> getAllLeaveMas()
        {
            return supplierRepository.getAllLeaveMas();
        }
        public int CreateLeaveMas(LeaveMasViewModel leave)
        {
            return supplierRepository.CreateLeaveMas(leave);
        }
        public List<ProjectPermissionViewModel> getAllPermissions()
        {
            return supplierRepository.getAllPermissions();
        }
        public int CreateMenu(int projectid, List<int> employeeNames)
        {
            return supplierRepository.CreateMenu(projectid, employeeNames);
        }
        public List<TimeEntryMasterViewModel> getAllTimeEntry(int userid, int groupid)
        {
            return supplierRepository.getAllTimeEntry(userid, groupid);
        }
        public List<TimeEntryMasterViewModel> getAllTimeEntries()
        {
            return supplierRepository.getAllTimeEntries();
        }
        public TimeEntrySubmitViewModel  getTimeEntrySubmit(int TimeEntryId)
        {

            return supplierRepository.getTimeEntrySubmit(TimeEntryId);
        }
        public int ApproveRejectTimeEntrySubmit(TimeEntrySubmitViewModel TM)
        {
            return supplierRepository.ApproveRejectTimeEntrySubmit(TM);
        }
        public int ApproveTimeEntry(int TM)
        {
            return supplierRepository.ApproveTimeEntry(TM);
        }
        public int RejectTimeEntry(int TM, string remark)
        {
            return supplierRepository.RejectTimeEntry(TM, remark);
        }
        public List<countryViewModel> getAllcountry()
        {
            return supplierRepository.getAllcountry();
        }
        public TimeEntryMasterViewModel getTimeEntry(int id)
        {

            return supplierRepository.getTimeEntry(id);
        }
        public int SaveTimeEntry(TimeEntrySubmitViewModel tm)
        {
            return supplierRepository.SaveTimeEntry(tm);
        }

        public int SaveDraft(TimeEntryMasterViewModel tevm)
        {
            return supplierRepository.SaveDraft(tevm);
        }

        public int Creategrid(TimeEntryMasterViewModel tevm)
        {
            return supplierRepository.Creategrid(tevm);
        }

        public TimeEntryMasterViewModel getTEMaster(int EID, DateTime temonth)
        {
            return supplierRepository.getTEMaster(EID, temonth);
        }

    }
}