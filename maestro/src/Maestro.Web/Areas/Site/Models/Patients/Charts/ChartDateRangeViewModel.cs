using System;
using Maestro.Web.Extensions;
using Maestro.Web.Helpers;

namespace Maestro.Web.Areas.Site.Models.Patients.Charts
{
    /// <summary>
    /// ChartRangeViewModel.
    /// </summary>
    public class ChartDateRangeViewModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ChartDateRangeViewModel"/> class.
        /// </summary>
        public ChartDateRangeViewModel()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ChartDateRangeViewModel"/> class.
        /// </summary>
        /// <param name="startDate">The start date.</param>
        /// <param name="endDate">The end date.</param>
        public ChartDateRangeViewModel(DateTime startDate, DateTime endDate)
        {
            StartDate = startDate;
            EndDate = endDate;
        }

        /// <summary>
        /// Gets or sets the start date.
        /// </summary>
        /// <value>
        /// The start date.
        /// </value>
        public DateTime? StartDate { get; set; }

        /// <summary>
        /// Gets or sets the end date.
        /// </summary>
        /// <value>
        /// The end date.
        /// </value>
        public DateTime? EndDate { get; set; }

        /// <summary>
        /// Merges two date time ranges 
        /// </summary>
        /// <param name="range1">The first range </param>
        /// <param name="range2">the second range</param>
        /// <returns></returns>
        public static ChartDateRangeViewModel Merge(ChartDateRangeViewModel range1, ChartDateRangeViewModel range2)
        {
            if (range1 == null || range2 == null)
            { 
                throw new ArgumentException("Invalid date time range");
            }
            var resultRange = new ChartDateRangeViewModel();

            if (!range1.StartDate.HasValue || !range2.StartDate.HasValue)
            {
                resultRange.StartDate = null;
            }
            else
            {
                resultRange.StartDate = DateTimeHelper.Min(range1.StartDate.Value, range2.StartDate.Value);
            }

            if (!range1.EndDate.HasValue || !range2.EndDate.HasValue)
            {
                resultRange.EndDate = null;
            }
            else
            {
                resultRange.EndDate = DateTimeHelper.Max(range1.EndDate.Value, range2.EndDate.Value);
            }

            return resultRange;
        }

        /// <summary>
        /// Compares for equality with the incoming date time range.
        /// </summary>
        /// <param name="obj">The incoming date time range.</param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            var inRange = obj as ChartDateRangeViewModel;

            if (inRange == null) return false;

            return this.StartDate.Equals(inRange.StartDate) && this.EndDate.Equals(inRange.EndDate);
        }

        /// <summary>
        /// Check if current date range is included in inDateRange
        /// </summary>
        /// <param name="inDateRange"></param>
        /// <returns></returns>
        public bool IsIn(ChartDateRangeViewModel inDateRange)
        {
            if (inDateRange == null) return false;           
       
            return this.StartDate.GreaterOrEqualThan(inDateRange.StartDate) &&
                   this.EndDate.LessOrEqualThan(inDateRange.EndDate);
        }

        /// <summary>
        /// Check if current date range intersects with inDateRange
        /// </summary>
        /// <param name="inDateRange"></param>
        /// <returns></returns>
        public bool IntersectsWith(ChartDateRangeViewModel inDateRange)
        {
            if (inDateRange == null) return false;

            return (this.StartDate.GreaterOrEqualThan(inDateRange.StartDate) && this.StartDate.LessOrEqualThan(inDateRange.EndDate)) ||
                   (this.EndDate.GreaterOrEqualThan(inDateRange.StartDate) && this.EndDate.LessOrEqualThan(inDateRange.EndDate));
        }

        public ChartDateRangeViewModel Substract(ChartDateRangeViewModel inDateRange)
        {
            if (inDateRange == null) return this;

            var resultDateRange = new ChartDateRangeViewModel();

            if (this.StartDate.GreaterOrEqualThan(inDateRange.StartDate) && this.StartDate.LessOrEqualThan(inDateRange.EndDate))
            {
                resultDateRange.StartDate = inDateRange.EndDate;
            }
            else
            {
                resultDateRange.StartDate = this.StartDate;
            }

            if (this.EndDate.GreaterOrEqualThan(inDateRange.StartDate) && this.EndDate.LessOrEqualThan(inDateRange.EndDate))
            {
                resultDateRange.EndDate = inDateRange.StartDate;
            }
            else
            {
                resultDateRange.EndDate = this.EndDate;
            }

            return resultDateRange;
        }
    }
}