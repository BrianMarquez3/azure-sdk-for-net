// <auto-generated>
// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for
// license information.
//
// Code generated by Microsoft (R) AutoRest Code Generator.
// Changes may cause incorrect behavior and will be lost if the code is
// regenerated.
// </auto-generated>

namespace Microsoft.Azure.Management.BatchAI.Models
{
    using Microsoft.Rest;
    using Newtonsoft.Json;
    using System.Linq;

    /// <summary>
    /// Information about docker image for the job.
    /// </summary>
    public partial class ImageSourceRegistry
    {
        /// <summary>
        /// Initializes a new instance of the ImageSourceRegistry class.
        /// </summary>
        public ImageSourceRegistry()
        {
            CustomInit();
        }

        /// <summary>
        /// Initializes a new instance of the ImageSourceRegistry class.
        /// </summary>
        /// <param name="image">Image.</param>
        /// <param name="serverUrl">Server URL.</param>
        /// <param name="credentials">Credentials.</param>
        public ImageSourceRegistry(string image, string serverUrl = default(string), PrivateRegistryCredentials credentials = default(PrivateRegistryCredentials))
        {
            ServerUrl = serverUrl;
            Image = image;
            Credentials = credentials;
            CustomInit();
        }

        /// <summary>
        /// An initialization method that performs custom operations like setting defaults
        /// </summary>
        partial void CustomInit();

        /// <summary>
        /// Gets or sets server URL.
        /// </summary>
        /// <remarks>
        /// URL for image repository.
        /// </remarks>
        [JsonProperty(PropertyName = "serverUrl")]
        public string ServerUrl { get; set; }

        /// <summary>
        /// Gets or sets image.
        /// </summary>
        /// <remarks>
        /// The name of the image in the image repository.
        /// </remarks>
        [JsonProperty(PropertyName = "image")]
        public string Image { get; set; }

        /// <summary>
        /// Gets or sets credentials.
        /// </summary>
        /// <remarks>
        /// Credentials to access the private docker repository.
        /// </remarks>
        [JsonProperty(PropertyName = "credentials")]
        public PrivateRegistryCredentials Credentials { get; set; }

        /// <summary>
        /// Validate the object.
        /// </summary>
        /// <exception cref="ValidationException">
        /// Thrown if validation fails
        /// </exception>
        public virtual void Validate()
        {
            if (Image == null)
            {
                throw new ValidationException(ValidationRules.CannotBeNull, "Image");
            }
            if (Credentials != null)
            {
                Credentials.Validate();
            }
        }
    }
}
