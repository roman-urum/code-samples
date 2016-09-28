using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using Maestro.Domain.Dtos.VitalsService.Enums;

namespace Maestro.Web.Helpers
{
    public class AlertTypeComparer: IComparer<AlertType>
    {
        private AlertType[] alerTypesPriorities = {
            AlertType.Insight,
            AlertType.Adherence,
            AlertType.ResponseViolation,
            AlertType.VitalsViolation
        };

        public int Compare(AlertType x, AlertType y)
        {

            int xIndex = Array.IndexOf(alerTypesPriorities, x);
            int yIndex = Array.IndexOf(alerTypesPriorities, y);

            return xIndex.CompareTo(yIndex);
        }
    }
}