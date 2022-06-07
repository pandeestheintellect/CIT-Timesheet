using Eng360Web.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Eng360Web.Models.Repository.Imp
{
    public interface ISupplierRepository
    {


        int CreateSupplier(SupplierViewModel supplier);
        int SaveSupplier(SupplierViewModel supplier);
        int DeleteSupplier(int supplierID);
        SupplierViewModel getSupplier(int supplierID);
        List<SupplierViewModel> getAllSuppliers();
        List<LeaveSettingViewModel> getAllLeaveSettings();
        int DeleteLeaveSetting(int leaveID);
        LeaveSettingViewModel getLeaveSetting(int leaveID);
        int SaveLeaveSetting(LeaveSettingViewModel leave);
        int CreateLeaveSetting(LeaveSettingViewModel leave);
        List<GroupViewModel> getAllGroups();
        int CreateMenu(int projectid, List<int> employeeNames);

        int DeleteGroup(int GroupID);
        GroupViewModel getGroup(int GroupId);
        int CreateGroup(GroupViewModel Group);
        int SaveGroup(GroupViewModel Group);
        List<ProjectNewViewModel> getAllProjects();
        int CreateProject(ProjectNewViewModel project);
        int DeleteProject(int projectID);
        ProjectNewViewModel getProject(int ProjectId);
        int SaveProject(ProjectNewViewModel project);
        List<HolidayMasterViewModel> getAllHoliday();
        int DeleteHoliday(int holidayID);
        HolidayMasterViewModel getHoliday(int HolidayId);
        int CreateHoliday(HolidayMasterViewModel holiday);
        int SaveHoliday(HolidayMasterViewModel Holiday);
        List<LeaveMasViewModel> getAllLeaveMas();
        int CreateLeaveMas(LeaveMasViewModel leave);
        List<ProjectPermissionViewModel> getAllPermissions();
        List<TimeEntryMasterViewModel> getAllTimeEntry(int userid, int groupid);
        int CreateTimeEntrySubmit(TimeEntrySubmitViewModel eng_time);
        TimeEntrySubmitViewModel getTimeEntrySubmit(int TimeEntryId);
        int ApproveRejectTimeEntrySubmit(TimeEntrySubmitViewModel TM);
        int ApproveTimeEntry(int TM);
        List<PEMappingViewModel> getAllProjectIDs();
        List<TimeEntryMasterViewModel> getAllTimeEntries();
        int RejectTimeEntry(int TM, string remark);
        List<countryViewModel> getAllcountry();
        TimeEntryMasterViewModel getTimeEntry(int id);
        int SaveTimeEntry(TimeEntrySubmitViewModel tm);

        
        TimeEntryMasterViewModel getTEMaster(int EID, DateTime temonth);
        int SaveDraft(TimeEntryMasterViewModel tevm);
        int Creategrid(TimeEntryMasterViewModel tevm);
    }
}