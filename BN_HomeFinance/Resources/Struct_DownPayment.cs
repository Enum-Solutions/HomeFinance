using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace BN_HomeFinance.Resources
{
    public class DownpaymentConstants
    {
        public const string Par_Value = "@Value";
        public const string Proc_GetDownpaymentValue = "GET_DOWNPAYMENT_VALUE";
        public const string Col_Value = "DownpaymentValue";
    }

    public class Downpayment : DownpaymentFactory
    {
        public int ID { get; set; }
        public int Value { get; set; }
    }

    public class DownpaymentFactory
    {
        public Downpayment GetLOV(string val)
        {
            DataTable dt = Helper.ExecuteSPWithParameters(DownpaymentConstants.Proc_GetDownpaymentValue, DownpaymentConstants.Par_Value, val);
            Downpayment objDownpayment = new Downpayment();
            if (dt != null)
            {
                objDownpayment.Value = Convert.ToInt32(dt.Rows[0][DownpaymentConstants.Col_Value]);
            }
            return objDownpayment;
        }
    }
}