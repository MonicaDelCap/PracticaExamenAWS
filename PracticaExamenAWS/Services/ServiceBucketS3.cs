using Amazon.S3.Model;
using Amazon.S3;
using System.Net;

namespace PracticaExamenAWS.Services
{
    public class ServiceBucketS3
    {
        private string BucketName;

        //LA CLASE/INTERFACE PARA TRABAJAR CON LOS BUCKETS
        //SE LLAMA IAmazonS3 Y VAMOS A RECIBIRLA MEDIANTE
        //INYECCION
        private IAmazonS3 ClientS3;

        public ServiceBucketS3(IConfiguration configuration, IAmazonS3 clientS3)
        {
            this.BucketName = "bucket-examen";
            this.ClientS3 = clientS3;
        }

        //COMENZAMOS CREANDO UN METODO PARA SUBIR FICHEROS
        public async Task<bool> UploadFileAsync(string fileName, Stream stream)
        {
            PutObjectRequest request = new PutObjectRequest
            {
                Key = fileName,
                BucketName = this.BucketName,
                InputStream = stream
            };
            //PARA TRABAJAR SE UTILIZA LA CLASE IAmazonS3 
            //CON UNA PETICION DE PUTOBJECT
            PutObjectResponse response = await this.ClientS3.PutObjectAsync(request);
            if (response.HttpStatusCode == HttpStatusCode.OK)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<bool> DeleteFileAsync(string fileName)
        {
            DeleteObjectResponse response = await this.ClientS3.DeleteObjectAsync(this.BucketName, fileName);
            if (response.HttpStatusCode == HttpStatusCode.OK)
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        public async Task<List<string>> GetVersionsFileAsync()
        {
            ListVersionsResponse response = await this.ClientS3.ListVersionsAsync(this.BucketName);

            List<string> fileNames = response.Versions.Select(x => x.Key).ToList();
            return fileNames;
        }

    }
}
