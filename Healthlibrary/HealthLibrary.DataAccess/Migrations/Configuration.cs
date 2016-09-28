using System.Data.Entity.Migrations;
using System.Linq;
using HealthLibrary.Common;
using HealthLibrary.Common.Helpers;
using HealthLibrary.DataAccess.Contexts;
using HealthLibrary.Domain.Entities.Element;
using HealthLibrary.Domain.Entities.Enums;

namespace HealthLibrary.DataAccess.Migrations
{
    /// <summary>
    /// Configuration.
    /// </summary>
    internal sealed class Configuration : DbMigrationsConfiguration<HealthLibraryServiceDbContext>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Configuration"/> class.
        /// </summary>
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        /// <summary>
        /// Seeds the specified context.
        /// </summary>
        /// <param name="context">The context.</param>
        protected override void Seed(HealthLibraryServiceDbContext context)
        {
            this.SeedMeasurementElements(context);
        }

        #region MeasurementElements

        /// <summary>
        /// Seeds the measurement elements.
        /// </summary>
        /// <param name="context">The context.</param>
        private void SeedMeasurementElements(HealthLibraryServiceDbContext context)
        {

            #region Measurements

            var measurementElement1 = new MeasurementElement
            {
                Id = SequentialGuidGenerator.Generate(),
                MeasurementType = MeasurementType.BloodGlucose,
                Name = "Glucose Meter",
                Type = ElementType.Measurement,
                CustomerId = Settings.CICustomerId
            };

            var measurementElement2 = new MeasurementElement
            {
                Id = SequentialGuidGenerator.Generate(),
                MeasurementType = MeasurementType.OxygenSaturation,
                Name = "Pulse Oximeter",
                Type = ElementType.Measurement,
                CustomerId = Settings.CICustomerId
            };

            var measurementElement3 = new MeasurementElement
            {
                Id = SequentialGuidGenerator.Generate(),
                MeasurementType = MeasurementType.Temperature,
                Name = "Thermometer",
                Type = ElementType.Measurement,
                CustomerId = Settings.CICustomerId
            };

            var measurementElement4 = new MeasurementElement
            {
                Id = SequentialGuidGenerator.Generate(),
                MeasurementType = MeasurementType.BloodPressure,
                Name = "Blood Pressure Monitor",
                Type = ElementType.Measurement,
                CustomerId = Settings.CICustomerId
            };

            var measurementElement5 = new MeasurementElement
            {
                Id = SequentialGuidGenerator.Generate(),
                MeasurementType = MeasurementType.PeakFlow,
                Name = "Peak Flow Meter",
                Type = ElementType.Measurement,
                CustomerId = Settings.CICustomerId
            };

            var measurementElement6 = new MeasurementElement
            {
                Id = SequentialGuidGenerator.Generate(),
                MeasurementType = MeasurementType.Weight,
                Name = "Weight Scale",
                Type = ElementType.Measurement,
                CustomerId = Settings.CICustomerId
            };

            var measurementElement7 = new MeasurementElement
            {
                Id = SequentialGuidGenerator.Generate(),
                MeasurementType = MeasurementType.Pedometer,
                Name = "Pedometer",
                Type = ElementType.Measurement,
                CustomerId = Settings.CICustomerId
            };

            if (!IsMeasurementExists(context, measurementElement1))
            {
                context.MeasurementElements.AddOrUpdate(measurementElement1);
            }

            if (!IsMeasurementExists(context, measurementElement2))
            {
                context.MeasurementElements.AddOrUpdate(measurementElement2);
            }

            if (!IsMeasurementExists(context, measurementElement3))
            {
                context.MeasurementElements.AddOrUpdate(measurementElement3);
            }

            if (!IsMeasurementExists(context, measurementElement4))
            {
                context.MeasurementElements.AddOrUpdate(measurementElement4);
            }

            if (!IsMeasurementExists(context, measurementElement5))
            {
                context.MeasurementElements.AddOrUpdate(measurementElement5);
            }

            if (!IsMeasurementExists(context, measurementElement6))
            {
                context.MeasurementElements.AddOrUpdate(measurementElement6);
            }

            if (!IsMeasurementExists(context, measurementElement7))
            {
                context.MeasurementElements.AddOrUpdate(measurementElement7);
            }

            #endregion

            #region Assessments

            var assessmentElement1 = new AssessmentElement
            {
                Id = SequentialGuidGenerator.Generate(),
                AssessmentType = AssessmentType.Stethoscope,
                Name = "Stethoscope",
                Type = ElementType.Assessment,
                CustomerId = Settings.CICustomerId
            };

            if (!IsAssessmentExists(context, assessmentElement1))
            {
                context.AssessmentElements.AddOrUpdate(assessmentElement1);
            }

            #endregion

            #region Answer Sets

            if (!IsOpenEndedAnswerSetExists(context, Settings.CICustomerId))
            {
                var openEndedAnswerSet = new AnswerSet
                {
                    CustomerId = Settings.CICustomerId,
                    IsDeleted = false,
                    Name = "Open Ended Answer Set",
                    Type = AnswerSetType.OpenEnded
                };

                context.AnswerSets.Add(openEndedAnswerSet);
            }

            #endregion

            context.SaveChanges();
        }

        /// <summary>
        /// Determines if open ended answer-set exists for specified customer.
        /// </summary>
        /// <returns></returns>
        public bool IsOpenEndedAnswerSetExists(HealthLibraryServiceDbContext context, int customerId)
        {
            return context.AnswerSets.Any(a => a.Type == AnswerSetType.OpenEnded && a.CustomerId == customerId);
        }

        /// <summary>
        /// Determines if measurement already exists in context.
        /// </summary>
        /// <param name="context"></param>
        /// <param name="element"></param>
        /// <returns></returns>
        public bool IsMeasurementExists(HealthLibraryServiceDbContext context, MeasurementElement element)
        {
            return
                context.MeasurementElements.Any(
                    e => e.CustomerId == element.CustomerId && e.MeasurementType == element.MeasurementType);
        }

        /// <summary>
        /// Determines if Assessment already exists in context.
        /// </summary>
        /// <param name="context"></param>
        /// <param name="element"></param>
        /// <returns></returns>
        public bool IsAssessmentExists(HealthLibraryServiceDbContext context, AssessmentElement element)
        {
            return
                context.AssessmentElements.Any(
                    e => e.CustomerId == element.CustomerId && e.AssessmentTypeString.Equals(element.AssessmentTypeString));
        }


        #endregion
    }
}