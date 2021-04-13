using Amazon;
using Amazon.Runtime;
using Amazon.S3;
using Amazon.S3.Model;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Intex2.Services
{
    public class S3Service : IS3Service
    {
        private readonly AmazonS3Client s3Client;
        private const string BUCKET_NAME = "arn:aws:s3:us-east-1:891046424969:accesspoint/photoupload";
        private const string FOLDER_NAME = "Photos";
        private const double DURATION = 24;
        public S3Service()
        {
            var s3Config = new AmazonS3Config{
                RegionEndpoint = RegionEndpoint.USEast1
            };

            var myCreds = new BasicAWSCredentials("AKIA465UBOGE5PFOHK7R", "UptX7iCA+Ozax3Uq3OshiWAAYIVJFVqYZaQZvu6w");

            AmazonS3Client S3Client = new AmazonS3Client(myCreds, s3Config);

            s3Client = S3Client;
        }
        
        public async Task<string> AddItem(IFormFile file, string ReaderName)
        {
            string fileName = file.FileName;
            string ObjectKey = $"{FOLDER_NAME}/{fileName}";

            using (Stream fileToUpload = file.OpenReadStream())
            {
                var putObjectRequest = new PutObjectRequest();
                putObjectRequest.BucketName = BUCKET_NAME;
                putObjectRequest.Key = ObjectKey;
                putObjectRequest.InputStream = fileToUpload;
                putObjectRequest.ContentType = file.ContentType;
                putObjectRequest.CannedACL = S3CannedACL.PublicRead;

                var response = await s3Client.PutObjectAsync(putObjectRequest);

                return fileName;

            }

            //private string GenerratedPreSignedURL(string ObjectKey)
            //{
            //    var request = new GetPreSignedUrlRequest
            //    {
            //        BucketName = BUCKET_NAME,
            //        Key = ObjectKey,
            //        Verb = HttpVerb.GET,
            //        Expires = DateTime.UtcNow.AddHours(DURATION)
            //    };
            //}

        }
    }
}
