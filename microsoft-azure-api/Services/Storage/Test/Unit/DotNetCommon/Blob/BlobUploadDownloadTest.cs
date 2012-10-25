﻿// -----------------------------------------------------------------------------------------
// <copyright file="BlobUploadDownloadTest.cs" company="Microsoft">
//    Copyright 2012 Microsoft Corporation
// 
//    Licensed under the Apache License, Version 2.0 (the "License");
//    you may not use this file except in compliance with the License.
//    You may obtain a copy of the License at
//      http://www.apache.org/licenses/LICENSE-2.0
// 
//    Unless required by applicable law or agreed to in writing, software
//    distributed under the License is distributed on an "AS IS" BASIS,
//    WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
//    See the License for the specific language governing permissions and
//    limitations under the License.
// </copyright>
// -----------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Microsoft.WindowsAzure.Storage.Blob
{
    [TestClass]
    public class BlobUploadDownloadTest : BlobTestBase
    {
        [TestMethod]
        [Description("Download a specific range of the blob")]
        [TestCategory(ComponentCategory.Blob)]
        [TestCategory(TestTypeCategory.UnitTest)]
        [TestCategory(SmokeTestCategory.NonSmoke)]
        [TestCategory(TenantTypeCategory.DevStore), TestCategory(TenantTypeCategory.DevFabric), TestCategory(TenantTypeCategory.Cloud)]
        public void PageBlobDownloadToStreamRangeTest()
        {
            byte[] buffer = GetRandomBuffer(2 * 1024);
            CloudBlobContainer container = GetRandomContainerReference();
            try
            {
                container.Create();

                CloudPageBlob blob = container.GetPageBlobReference("blob1");
                using (MemoryStream wholeBlob = new MemoryStream(buffer))
                {
                    blob.UploadFromStream(wholeBlob);

                    byte[] testBuffer = new byte[1024];
                    MemoryStream blobStream = new MemoryStream(testBuffer);
                    StorageException storageEx = TestHelper.ExpectedException<StorageException>(
                        () => blob.DownloadRangeToStream(blobStream, 0, 0),
                        "Requesting 0 bytes when downloading range should not work");
                    Assert.IsInstanceOfType(storageEx.InnerException, typeof(ArgumentOutOfRangeException));
                    blob.DownloadRangeToStream(blobStream, 0, 1024);
                    Assert.AreEqual(blobStream.Position, 1024);
                    TestHelper.AssertStreamsAreEqualAtIndex(blobStream, wholeBlob, 0, 0, 1024);

                    CloudPageBlob blob2 = container.GetPageBlobReference("blob1");
                    MemoryStream blobStream2 = new MemoryStream(testBuffer);
                    storageEx = TestHelper.ExpectedException<StorageException>(
                        () => blob2.DownloadRangeToStream(blobStream, 1024, 0),
                        "Requesting 0 bytes when downloading range should not work");
                    Assert.IsInstanceOfType(storageEx.InnerException, typeof(ArgumentOutOfRangeException));
                    blob2.DownloadRangeToStream(blobStream2, 1024, 1024);
                    TestHelper.AssertStreamsAreEqualAtIndex(blobStream2, wholeBlob, 0, 1024, 1024);

                    AssertAreEqual(blob, blob2);
                }
            }
            finally
            {
                container.DeleteIfExists();
            }
        }

        [TestMethod]
        [Description("Upload a stream to a blob")]
        [TestCategory(ComponentCategory.Blob)]
        [TestCategory(TestTypeCategory.UnitTest)]
        [TestCategory(SmokeTestCategory.NonSmoke)]
        [TestCategory(TenantTypeCategory.DevStore), TestCategory(TenantTypeCategory.DevFabric), TestCategory(TenantTypeCategory.Cloud)]
        public void BlobUploadFromStreamTest()
        {
            byte[] buffer = GetRandomBuffer(2 * 1024);
            CloudBlobContainer container = GetRandomContainerReference();
            try
            {
                container.Create();

                CloudPageBlob blob = container.GetPageBlobReference("blob1");
                using (MemoryStream srcStream = new MemoryStream(buffer))
                {
                    blob.UploadFromStream(srcStream);
                    byte[] testBuffer = new byte[2048];
                    MemoryStream dstStream = new MemoryStream(testBuffer);
                    blob.DownloadRangeToStream(dstStream, null, null);
                    TestHelper.AssertStreamsAreEqual(srcStream, dstStream);
                }
            }
            finally
            {
                container.DeleteIfExists();
            }
        }

        [TestMethod]
        [Description("Upload from text to a blob")]
        [TestCategory(ComponentCategory.Blob)]
        [TestCategory(TestTypeCategory.UnitTest)]
        [TestCategory(SmokeTestCategory.NonSmoke)]
        [TestCategory(TenantTypeCategory.DevStore), TestCategory(TenantTypeCategory.DevFabric), TestCategory(TenantTypeCategory.Cloud)]
        public void BlobUploadWithoutMD5ValidationAndStoreBlobContentTest()
        {   
            byte[] buffer = GetRandomBuffer(2 * 1024);
            CloudBlobContainer container = GetRandomContainerReference();
            try
            {
                container.Create();

                CloudPageBlob blob = container.GetPageBlobReference("blob1");
                BlobRequestOptions options = new BlobRequestOptions();
                options.DisableContentMD5Validation = false;
                options.StoreBlobContentMD5 = false;
                OperationContext context = new OperationContext();
                using (MemoryStream srcStream = new MemoryStream(buffer))
                {
                    blob.UploadFromStream(srcStream, null, options, context);
                    blob.FetchAttributes();
                    string md5 = blob.Properties.ContentMD5;
                    blob.Properties.ContentMD5 = "MDAwMDAwMDA=";
                    blob.SetProperties(null, options, context);
                    byte[] testBuffer = new byte[2048];
                    MemoryStream dstStream = new MemoryStream(testBuffer);
                    TestHelper.ExpectedException(() => blob.DownloadRangeToStream(dstStream, null, null, null, options, context), 
                        "Try to Download a stream with a corrupted md5 and DisableMD5Validation set to false",
                        HttpStatusCode.OK);

                    options.DisableContentMD5Validation = true;
                    blob.SetProperties(null, options, context);
                    byte[] testBuffer2 = new byte[2048];
                    MemoryStream dstStream2 = new MemoryStream(testBuffer2);
                    blob.DownloadRangeToStream(dstStream2, null, null, null, options, context);
                }
            }
            finally
            {
                container.DeleteIfExists();
            }
        }

        [TestMethod]
        [Description("Upload from text to a blob")]
        [TestCategory(ComponentCategory.Blob)]
        [TestCategory(TestTypeCategory.UnitTest)]
        [TestCategory(SmokeTestCategory.NonSmoke)]
        [TestCategory(TenantTypeCategory.DevStore), TestCategory(TenantTypeCategory.DevFabric), TestCategory(TenantTypeCategory.Cloud)]
        public void BlobEmptyHeaderSigningTest()
        {
            byte[] buffer = GetRandomBuffer(2 * 1024);
            CloudBlobContainer container = GetRandomContainerReference();
            OperationContext context = new OperationContext();
            try
            {
                container.Create(null, context);
                CloudPageBlob blob = container.GetPageBlobReference("blob1");
                context.UserHeaders = new Dictionary<string, string>();
                context.UserHeaders.Add("x-ms-foo", String.Empty);
                using (MemoryStream srcStream = new MemoryStream(buffer))
                {
                    blob.UploadFromStream(srcStream, null, null, context);
                }
                byte[] testBuffer2 = new byte[2048];
                MemoryStream dstStream2 = new MemoryStream(testBuffer2);
                blob.DownloadRangeToStream(dstStream2, null, null, null, null, context);
            }
            finally
            {
                container.DeleteIfExists();
            }
        }

    }
}