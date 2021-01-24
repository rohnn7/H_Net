using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Threading.Tasks;
using H_Net.Data_Contract;


namespace H_Net.ServiceContract
{
    [ServiceContract]
    public interface IServiceContract
    {
        #region Register Photo
        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "photoprocessing",
            BodyStyle = WebMessageBodyStyle.Bare,
            ResponseFormat = WebMessageFormat.Json,
            RequestFormat = WebMessageFormat.Json)]
        [FaultContract(typeof(CustomException))]
        string ProcessPhoto(Param p);
        #endregion
        #region Register User
        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "RegisterUser",
            BodyStyle = WebMessageBodyStyle.Bare,
            ResponseFormat = WebMessageFormat.Json,
            RequestFormat = WebMessageFormat.Json)]
        [FaultContract(typeof(CustomException))]
        int RegisterUser(Param p);
        #endregion
        #region Check User
        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "checkuser",
            BodyStyle = WebMessageBodyStyle.Bare,
            ResponseFormat = WebMessageFormat.Json,
            RequestFormat = WebMessageFormat.Json)]
        [FaultContract(typeof(CustomException))]
        bool CheckUser(Param p);
        #endregion
        #region Get Countries
        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "ProfileDetails",
            BodyStyle = WebMessageBodyStyle.Bare,
            ResponseFormat = WebMessageFormat.Json,
            RequestFormat = WebMessageFormat.Json)]
        [FaultContract(typeof(CustomException))]
        List<ProfileDetails> ProfileDetails(Param pr);
        #endregion
        //#region Get User Details
        //[OperationContract]
        //[WebInvoke(Method = "POST", UriTemplate = "UserDetails",
        //    BodyStyle = WebMessageBodyStyle.Bare,
        //    ResponseFormat = WebMessageFormat.Json,
        //    RequestFormat = WebMessageFormat.Json)]
        //[FaultContract(typeof(CustomException))]
        //List<Param> GetUserDetails(Param p);
        //#endregion
        //#region Get Countries
        //[OperationContract]
        //[WebInvoke(Method = "POST", UriTemplate = "countries",
        //    BodyStyle = WebMessageBodyStyle.Bare,
        //    ResponseFormat = WebMessageFormat.Json,
        //    RequestFormat = WebMessageFormat.Json)]
        //[FaultContract(typeof(CustomException))]
        //List<Address> getCountries(Param p);
        //#endregion
        //#region Insert/update User Address
        //[OperationContract]
        //[WebInvoke(Method = "POST", UriTemplate = "InsertUserAddress",
        //    BodyStyle = WebMessageBodyStyle.Bare,
        //    ResponseFormat = WebMessageFormat.Json,
        //    RequestFormat = WebMessageFormat.Json)]
        //[FaultContract(typeof(CustomException))]
        //int InsertUserAddress(Param p);
        //#endregion
        //#region Get User Address
        //[OperationContract]
        //[WebInvoke(Method = "POST", UriTemplate = "GetUserDetails",
        //    BodyStyle = WebMessageBodyStyle.Bare,
        //    ResponseFormat = WebMessageFormat.Json,
        //    RequestFormat = WebMessageFormat.Json)]
        //[FaultContract(typeof(CustomException))]
        //List<Address> GetAddresses(Param p);
        //#endregion
        //#region Delete User
        //[OperationContract]
        //[WebInvoke(Method = "POST", UriTemplate = "DeleteUser",
        //    BodyStyle = WebMessageBodyStyle.Bare,
        //    ResponseFormat = WebMessageFormat.Json,
        //    RequestFormat = WebMessageFormat.Json)]
        //[FaultContract(typeof(CustomException))]
        //int DeleteUser(Param p);
        //#endregion
    }
}
