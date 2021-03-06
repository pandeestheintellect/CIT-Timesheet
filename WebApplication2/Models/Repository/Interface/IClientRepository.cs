using Eng360Web.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Eng360Web.Models.Repository.Interface
{
    public interface IClientRepository
    {
        List<ClientViewModel> getAllClients();
        ClientViewModel getClient(int clientID);
        List<FunctionViewModel> getAllFunctions();
        List<IndustryViewModel> getAllIndustries();
        int CreateClient(ClientViewModel eng_client_master);
        int SaveClient(ClientViewModel client);
        int DeleteClient(int clientID);
        //ClientContactViewModel getContact(int? id);
        
    }
}