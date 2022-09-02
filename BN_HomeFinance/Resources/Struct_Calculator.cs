using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BN_HomeFinance.Resources
{
    public class CalculatorConstants
    {
        public const string proc_GetDownpayments = "GET_DOWNPAYMENTS";
        public const string proc_GetTenure = "GET_TENURE";
    }
    public class Calculator
    {

        private Tenure tenure = new Tenure();
        public Tenure Tenure
        {
            get { return tenure; }
            set { tenure = value; }
        }

        private Downpayment downpayment = new Downpayment();
        public Downpayment DownPayment
        {
            get { return downpayment; }
            set { downpayment = value; }
        }

    }
    public class CalculatorFactory
    {

    }
}