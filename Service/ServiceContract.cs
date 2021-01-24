using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Web;
using H_Net.Data_Contract;
using H_Net.ServiceContract;

namespace H_Net.Service
{
    public class ServiceContract: IServiceContract
    {
        #region Create User
        public int RegisterUser(Param p)
        {

            bool success = false;
            int clientID = 0;
            Data_Access.Data_Access da = new Data_Access.Data_Access();
            if (!da.authenticateUser(p))
            {
                CustomException ce = new CustomException();
                ce.o_statusCode = 1;
                ce.o_statusMessage = "User is not authorised to access the service";
                ce.Title = "Unauthroised Access Denied";
                throw new FaultException<CustomException>(ce, "Reason : Unauthorised Access");
            }
            else
            {
                string o_statusMessage = "";
                int o_status = -1;
                clientID = da.RegisterUser(p, out o_status, out o_statusMessage, 1);
                if (o_status != 0)
                {
                    success = false;
                    clientID = 0;
                    CustomException ce = new CustomException();
                    ce.o_statusCode = o_status;
                    ce.o_statusMessage = o_statusMessage;
                    ce.Title = "Exception Occured in Registering the User";
                    throw new FaultException<CustomException>(ce, "Reason : Error Occured");
                }
            }
            return clientID;

        }
        #endregion
        #region Check User
        public bool CheckUser(Param p)
        {

            bool success = false;
            bool clientID = false;
            Data_Access.Data_Access da = new Data_Access.Data_Access();
            if (!da.authenticateUser(p))
            {
                CustomException ce = new CustomException();
                ce.o_statusCode = 1;
                ce.o_statusMessage = "User is not authorised to access the service";
                ce.Title = "Unauthroised Access Denied";
                throw new FaultException<CustomException>(ce, "Reason : Unauthorised Access");
            }
            else
            {
                string o_statusMessage = "";
                int o_status = -1;
                clientID = da.checkUser(p, out o_status, out o_statusMessage, 1);
                if (clientID)
                {
                    return true;
                }
                else
                {
                    return false;
                }
                /* if (o_status != 0)
                 {
                     success = false;
                     clientID = false;
                     CustomException ce = new CustomException();
                     ce.o_statusCode = o_status;
                     ce.o_statusMessage = o_statusMessage;
                     ce.Title = "Exception Occured in Registering the User";
                     throw new FaultException<CustomException>(ce, "Reason : Error Occured");
                 }*/
            }


        }
        #endregion
        #region Process Photo
        public string ProcessPhoto(Param p)
        {

            bool success = false;
            int clientID = 0;
            string email = "";
            Data_Access.Data_Access da = new Data_Access.Data_Access();
            if (!da.authenticateUser(p))
            {
                CustomException ce = new CustomException();
                ce.o_statusCode = 1;
                ce.o_statusMessage = "User is not authorised to access the service";
                ce.Title = "Unauthroised Access Denied";
                throw new FaultException<CustomException>(ce, "Reason : Unauthorised Access");
            }
            else
            {
                string o_statusMessage = "";
                int o_status = -1;
                email= da.Processphoto(p, out o_status, out o_statusMessage, 1);
                if (o_status != 0)
                {
                    success = false;
                    clientID = 0;
                    CustomException ce = new CustomException();
                    ce.o_statusCode = o_status;
                    ce.o_statusMessage = o_statusMessage;
                    ce.Title = "Exception Occured in Registering the User";
                    throw new FaultException<CustomException>(ce, "Reason : Error Occured");
                }
            }
            return email;

        }
        #endregion
        #region get countries
        public List<ProfileDetails> ProfileDetails(Param p)
        {
            List<ProfileDetails> cList = new List<ProfileDetails>();
            Data_Access.Data_Access da = new Data_Access.Data_Access();
            if (!da.authenticateUser(p))
            {
                CustomException ce = new CustomException();
                ce.o_statusCode = 1;
                ce.o_statusMessage = "User is not authorised to access the service";
                ce.Title = "Unauthroised Access Denied";
                throw new FaultException<CustomException>(ce, "Reason : Unauthorised Access");
            }
            else
            {
                string o_statusMessage = "";
                int o_status = -1;
                cList = da.ProfileDetails(p,out o_status, out o_statusMessage, 1);
                if (o_status != 0)
                {
                    CustomException ce = new CustomException();
                    ce.o_statusCode = o_status;
                    ce.o_statusMessage = o_statusMessage;
                    ce.Title = "Exception Occured in Getting COuntries";
                    throw new FaultException<CustomException>(ce, "Reason : Error Occured");
                }
            }
            return cList;
        }
        #endregion
    }
}
