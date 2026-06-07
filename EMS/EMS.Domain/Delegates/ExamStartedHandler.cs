using EMS.Domain.EventArg;
using System;
using System.Collections.Generic;
using System.Text;

namespace EMS.Domain.Delegates
{
    public delegate void ExamStartedHandler(object sender, ExamEventArgs e);
}
