using System;
using System.Web;
using System.Drawing;
using DeviceService.Domain.Entities.Enums;
using DeviceService.Web.Api.DataAnnotations;
using DeviceService.Web.Api.Resources;
using MessagingToolkit.QRCode.Codec;

namespace DeviceService.Web.Api.Models.Dtos.Entities
{
    /// <summary>
    /// DeviceDto.
    /// </summary>
    public class DeviceDto
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the patient identifier.
        /// </summary>
        /// <value>
        /// The patient identifier.
        /// </value>
        public Guid PatientId { get; set; }

        /// <summary>
        /// Gets or sets the customer identifier.
        /// </summary>
        /// <value>
        /// The customer identifier.
        /// </value>
        public int CustomerId { get; set; }

        /// <summary>
        /// Gets or sets the status.
        /// </summary>
        /// <value>
        /// The status.
        /// </value>
        public Status Status { get; set; }

        /// <summary>
        /// Gets or sets the device identifier.
        /// </summary>
        /// <value>
        /// The device identifier.
        /// </value>
        public string DeviceId { get; set; }

        /// <summary>
        /// Gets or sets the type of the device.
        /// </summary>
        /// <value>
        /// The type of the device.
        /// </value>
        public string DeviceType { get; set; }

        /// <summary>
        /// Gets or sets the type of the device identifier.
        /// </summary>
        /// <value>
        /// The type of the device identifier.
        /// </value>
        public DeviceIdType? DeviceIdType { get; set; }

        /// <summary>
        /// Gets or sets the device model.
        /// </summary>
        /// <value>
        /// The device model.
        /// </value>
        public string DeviceModel { get; set; }

        /// <summary>
        /// Gets or sets the device timezone.
        /// </summary>
        /// <value>
        /// The device timezone.
        /// </value>
        public string DeviceTz { get; set; }

        /// <summary>
        /// Gets or sets the activation code.
        /// </summary>
        /// <value>
        /// The activation code.
        /// </value>
        public string ActivationCode { get; set; }

        /// <summary>
        /// Gets or sets the birth date.
        /// </summary>
        /// <value>
        /// The birth date.
        /// </value>
        [DateString(Format = "yyyy-MM-dd", ErrorMessageResourceType = typeof(GlobalStrings), ErrorMessageResourceName = "BirthDate_RecognizeDate")]
        public string BirthDate { get; set; }

        /// <summary>
        /// Date when device connected last time.
        /// </summary>
        public DateTime? LastConnectedUtc { get; set; }

        /// <summary>
        /// Gets the QR code.
        /// </summary>
        /// <value>
        /// The qr code.
        /// </value>
        public byte[] QRCode
        {
            get
            {
                QRCodeEncoder qrEncoder = new QRCodeEncoder();

                string activationUrl = string.Format("{0}/api/activation", HttpContext.Current.Request.Url.Host);
                Bitmap qrBitmap = qrEncoder.Encode(activationUrl);

                ImageConverter converter = new ImageConverter();

                return (byte[])converter.ConvertTo(qrBitmap, typeof(byte[]));
            }
        }

        /// <summary>
        /// Gets or sets the settings.
        /// </summary>
        /// <value>
        /// The settings.
        /// </value>
        public DeviceSettingsDto Settings { get; set; }
    }
}