﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kooboo.Model.ValidateRules
{
    public class EmailRule:Rule
    {
        public string Regex
        {
            get
            {
                //example can change ,this value is from front-end;
                return "^[-!#$%&\'*+\\./0-9=?A-Z^_`a-z{|}~]+@[-!#$%&\'*+\\/0-9=?A-Z^_`a-z{|}~]+\\.[-!#$%&\'*+\\./0-9=?A-Z^_`a-z{|}~]+$";
            }
        }

        public string _message;
        public override string Message
        {
            get
            {
                if(string.IsNullOrEmpty(_message))
                {
                    _message = "Email is error";
                }
                return "";
            }
            set
            {
                _message = value;
            }
        }

        public override string GetRule()
        {
            return "{}";
        }
    }
}