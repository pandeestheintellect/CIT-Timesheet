using Eng360Web.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Eng360Web.Models.Service.Imp
{
    public interface ISupplierServices
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
        List<ProjectNewViewModel> getAllProjects();


        int DeleteGroup(int GroupID);
        GroupViewModel getGroup(int GroupId);
        int CreateGroup(GroupViewModel Group);
        int SaveGroup(GroupViewModel Group);
        int CreateProject(ProjectNewViewModel project);
        int DeleteProject(int ProjectID);
        ProjectNewViewModel getProject(int ProjectId);
        int SaveProject(ProjectNewViewModel project);
        List<HolidayMasterViewModel> getAllHoliday();
        int DeleteHoliday(int HolidayID);
        HolidayMasterViewModel getHoliday(int HolidayID);
        int CreateHoliday(HolidayMasterViewModel holiday);
        int SaveHoliday(HolidayMasterViewModel holiday);
        List<LeaveMasViewModel> getAllLeaveMas();
        int CreateLeaveMas(LeaveMasViewModel leave);
        List<ProjectPermissionViewModel> getAllPermissions();
        int CreateMenu(int projectid, List<int> employeeNames);
        List<TimeEntryMasterViewModel> getAllTimeEntry(int userid, int groupid);
        List<TimeEntryMasterViewModel> getAllTimeEntries();
        List<PEMappingViewModel> getAllProjectIDs();
        int CreateTimeEntrySubmit(TimeEntrySubmitViewModel eng_time);
        TimeEntrySubmitViewModel getTimeEntrySubmit(int TimeEntryId);
        int ApproveRejectTimeEntrySubmit(TimeEntrySubmitViewModel TM);
        int ApproveTimeEntry(int TM);
        int RejectTimeEntry(int TM, string remark);
        List<countryViewModel> getAllcountry();
        TimeEntryMasterViewModel getTimeEntry(int id);
        int SaveTimeEntry(TimeEntrySubmitViewModel tm);
        int SaveDraft(TimeEntryMasterViewModel tevm);
        int Creategrid(TimeEntryMasterViewModel tevm);

        TimeEntryMasterViewModel getTEMaster(int EID, DateTime temonth);
    }
}