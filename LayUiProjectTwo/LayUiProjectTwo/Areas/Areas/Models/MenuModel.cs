using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LayUiProjectTwo.Areas.Areas.Models
{
    public class MenuModel
    {
        //SYS_PERMISSION_ID as ID,CNAME AS NAME,PID,CURL,IORDER

        string _ID;

        string _NAME;

        string _PID;

        string _CURL;

        Decimal _IORDER;

        List<MenuModel> _Childlist;

        public string ID
        {
            get
            {
                return _ID;
            }

            set
            {
                _ID = value;
            }
        }

        public string NAME
        {
            get
            {
                return _NAME;
            }

            set
            {
                _NAME = value;
            }
        }

        public string PID
        {
            get
            {
                return _PID;
            }

            set
            {
                _PID = value;
            }
        }

        public string CURL
        {
            get
            {
                return _CURL;
            }

            set
            {
                _CURL = value;
            }
        }

        public List<MenuModel> Childlist
        {
            get
            {
                return _Childlist;
            }

            set
            {
                _Childlist = value;
            }
        }
    }
}