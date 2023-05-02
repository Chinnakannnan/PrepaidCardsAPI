using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Net;

namespace BusinessModel.Common
{
    public class HTTPPOST
    {



     public const string lstrFolderName = "HTTP";

            public string POSTData(string lstrHTTPMethod, string URL, string APIKEY, string body, string ContentType)
            {
                Log objLog = new Log();
                string lstrOut = string.Empty;
                Dictionary<string, string> requestBody = new Dictionary<string, string>();
                string lstrPostData = string.Empty;
                try
                {
                    ServicePointManager.Expect100Continue = true;
                    ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12
                           | SecurityProtocolType.Tls11
                           | SecurityProtocolType.Tls12
                           | SecurityProtocolType.Ssl3
                           | (SecurityProtocolType)3072;

                    HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(URL);
                    httpWebRequest.Timeout = 60 * 1000;
                    httpWebRequest.ContentType = ContentType;// "application/json";
                    httpWebRequest.Method = lstrHTTPMethod;

                    //if(!string.IsNullOrWhiteSpace(body))
                    //{
                    //    requestBody.Add("Request", crypto.AES_ENCRYPT(body,COMMON.KEY));
                    //    lstrPostData = JsonConvert.SerializeObject(requestBody);
                    //}
                    //else
                    //{
                    //    lstrPostData = "";
                    //}

                    lstrPostData = body;

                    byte[] bytes = Encoding.UTF8.GetBytes(lstrPostData);
                    if (!string.IsNullOrEmpty(APIKEY))
                    {
                        httpWebRequest.Headers.Add("Authorization", APIKEY);
                    }

                    objLog.WriteAppLog(URL + ":" + lstrPostData + ":" + ContentType + ":" + APIKEY, lstrFolderName);

                    httpWebRequest.ProtocolVersion = HttpVersion.Version10;


                    using (System.IO.Stream sendStream = httpWebRequest.GetRequestStream())
                    {
                        sendStream.Write(bytes, 0, bytes.Length);
                        sendStream.Close();
                    }

                    HttpWebResponse myHttpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                    if (myHttpWebResponse.StatusCode == HttpStatusCode.OK)
                    {
                        //lstrOut = myHttpWebResponse.StatusDescription;
                    }

                    //System.Net.WebResponse res = httpWebRequest.GetResponse();
                    System.IO.Stream ReceiveStream = myHttpWebResponse.GetResponseStream();
                    using (System.IO.StreamReader sr = new System.IO.StreamReader(ReceiveStream, Encoding.UTF8))
                    {
                        lstrOut = sr.ReadToEnd();
                    }
                    return lstrOut;
                }
                catch (WebException ex)
                {
                    using (WebResponse response = ex.Response)
                    {
                        if (response != null)
                        {
                            HttpWebResponse httpResponse = (HttpWebResponse)response;
                            using (Stream data = response.GetResponseStream())
                            {
                                using (var reader = new StreamReader(data))
                                {
                                    lstrOut = reader.ReadToEnd();
                                    objLog.WriteErrorLog("POSTData Web exception :" + lstrOut, lstrFolderName);
                                }
                            }
                        }
                    }
                    objLog.WriteErrorLog("POSTData : Web exception :" + ex.ToString(), lstrFolderName);
                    lstrOut = "";
                    return lstrOut;
                }
                catch (Exception ex)
                {
                    objLog.WriteErrorLog("POSTData :" + ex.ToString(), lstrFolderName);
                    lstrOut = "";
                    return lstrOut;
                }
                finally
                {
                    requestBody = null;
                    objLog = null;
                }
            }
            public string GETData(string lstrHTTPMethod, string URL, string APIKEY, string body, string ContentType)
            {
                Log objLog = new Log();
                string lstrOut = string.Empty;
                try
                {
                    ServicePointManager.Expect100Continue = true;
                    ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12
                           | SecurityProtocolType.Tls11
                           | SecurityProtocolType.Tls12
                           | SecurityProtocolType.Ssl3
                           | (SecurityProtocolType)3072;

                    WebRequest webRequest = WebRequest.Create(URL);
                    webRequest.Credentials = CredentialCache.DefaultCredentials;
                    webRequest.Timeout = 40 * 1000;
                    webRequest.Method = "GET";
                    webRequest.Headers.Add("Authorization", APIKEY);

                    objLog.WriteAppLog(URL + ":" + APIKEY, lstrFolderName);

                    HttpWebResponse myHttpWebResponse = (HttpWebResponse)webRequest.GetResponse();
                    if (myHttpWebResponse.StatusCode == HttpStatusCode.OK)
                    {
                        //lstrOut = myHttpWebResponse.StatusDescription;
                    }

                    System.IO.Stream ReceiveStream = myHttpWebResponse.GetResponseStream();
                    using (System.IO.StreamReader sr = new System.IO.StreamReader(ReceiveStream, Encoding.UTF8))
                    {
                        lstrOut = sr.ReadToEnd();
                    }
                    return lstrOut;
                }
                catch (WebException ex)
                {
                    using (WebResponse response = ex.Response)
                    {
                        HttpWebResponse httpResponse = (HttpWebResponse)response;
                        using (Stream data = response.GetResponseStream())
                        using (var reader = new StreamReader(data))
                        {
                            lstrOut = reader.ReadToEnd();
                            objLog.WriteErrorLog("GETData exception :" + lstrOut, lstrFolderName);
                        }
                    }
                    objLog.WriteErrorLog("GETData :" + ex.ToString(), lstrFolderName);
                    lstrOut = "";
                    return lstrOut;
                }
            }

            public string POSTDatatoAadharURL(string URL, string body, string ContentType)
            {
                //return "{    \"status\": \"FAILURE\",    \"statusCode\": \"DE_010\",    \"statusMessage\": \"Failed\"}";
                //return "{    \"status\": \"ACCEPTED\",    \"statusCode\": \"DE_001\",    \"statusMessage\": \"Request accepted\"}";
                Log objLog = new Log();
                string lstrOut = string.Empty;
                try
                {
                    HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(URL);
                    httpWebRequest.ContentType = ContentType;// "application/json";
                    httpWebRequest.Method = "POST";
                    byte[] bytes = Encoding.UTF8.GetBytes(body);
                    httpWebRequest.Headers.Add("api-key", "EZKXK6Y-WXQ4ZK5-M0E4C3R-3BJS9B4");
                    httpWebRequest.Headers.Add("app-id", "63721c77224f1b001d344290");
                    ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;

                    using (System.IO.Stream sendStream = httpWebRequest.GetRequestStream())
                    {
                        sendStream.Write(bytes, 0, bytes.Length);
                        sendStream.Close();
                    }
                    System.Net.WebResponse res = httpWebRequest.GetResponse();
                    System.IO.Stream ReceiveStream = res.GetResponseStream();
                    using (System.IO.StreamReader sr = new System.IO.StreamReader(ReceiveStream, Encoding.UTF8))
                    {
                        lstrOut = sr.ReadToEnd();
                    }
                    return lstrOut;
                }
                catch (WebException ex)
                {
                    objLog.WriteErrorLog("SendPOSTData :" + ex.ToString(), lstrFolderName);
                    //lstrError = ex.ToString();
                    lstrOut = "";
                    return lstrOut;
                }
                catch (Exception ex)
                {
                    objLog.WriteErrorLog("SendPOSTData :" + ex.ToString(), lstrFolderName);
                    //lstrError = ex.ToString();
                    lstrOut = "";
                    return lstrOut;
                }
            }
            public string POSTDatabyKeyHeader(string lstrHTTPMethod, string URL, string body, string ContentType)
            {
                Log objLog = new Log();
                string lstrOut = string.Empty;
                Dictionary<string, string> requestBody = new Dictionary<string, string>();
                string lstrPostData = string.Empty;
                try
                {
                    ServicePointManager.Expect100Continue = true;
                    ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12
                           | SecurityProtocolType.Tls11
                           | SecurityProtocolType.Ssl3
                           | (SecurityProtocolType)3072;

                    HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(URL);
                    httpWebRequest.Timeout = 60 * 1000;
                    httpWebRequest.ContentType = ContentType;// "application/json";
                    httpWebRequest.Method = lstrHTTPMethod;

                    if (!string.IsNullOrWhiteSpace(body))
                    {
                        //requestBody.Add("Request", crypto.AES_ENCRYPT(body, ConfigurationManager.AppSettings["APIAESKEY"]));
                        //lstrPostData = JsonConvert.SerializeObject(requestBody);
                        // lstrPostData = crypto.AES_ENCRYPT(body, ConfigurationManager.AppSettings["APIAESKEY"]);
                        lstrPostData = body;
                    }
                    else
                    {
                        lstrPostData = body;
                    }


                    byte[] bytes = Encoding.UTF8.GetBytes(lstrPostData);
                    httpWebRequest.Headers.Add("api-key", "FJVTS74-RAAM91G-P2Y0DHG-3Z1QGW9");
                    httpWebRequest.Headers.Add("app-id", "62c7df712d4a96001d8dfb23");
                    objLog.WriteAppLog(URL + ":" + lstrPostData + ":" + ContentType + ":", lstrFolderName);

                    httpWebRequest.ProtocolVersion = HttpVersion.Version10;


                    using (System.IO.Stream sendStream = httpWebRequest.GetRequestStream())
                    {
                        sendStream.Write(bytes, 0, bytes.Length);
                        sendStream.Close();
                    }

                    HttpWebResponse myHttpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                    if (myHttpWebResponse.StatusCode == HttpStatusCode.OK)
                    {
                        //lstrOut = myHttpWebResponse.StatusDescription;
                    }

                    //System.Net.WebResponse res = httpWebRequest.GetResponse();
                    System.IO.Stream ReceiveStream = myHttpWebResponse.GetResponseStream();
                    using (System.IO.StreamReader sr = new System.IO.StreamReader(ReceiveStream, Encoding.UTF8))
                    {
                        lstrOut = sr.ReadToEnd();
                    }
                    return lstrOut;
                }
                catch (WebException ex)
                {
                    //using (WebResponse response = ex.Response)
                    //{
                    //    HttpWebResponse httpResponse = (HttpWebResponse)response;
                    //    using (Stream data = response.GetResponseStream())
                    //    {
                    //        using (var reader = new StreamReader(data))
                    //        {
                    //            lstrOut = reader.ReadToEnd();
                    //            objLog.WriteErrorLog("POSTData Web exception :" + lstrOut, lstrFolderName);
                    //        }
                    //    }
                    //}
                    objLog.WriteErrorLog("POSTData : Web exception :" + ex.ToString(), lstrFolderName);
                    lstrOut = "";
                    return lstrOut;
                }
                catch (Exception ex)
                {
                    objLog.WriteErrorLog("POSTData :" + ex.ToString(), lstrFolderName);
                    lstrOut = "";
                    return lstrOut;
                }
                finally
                {
                    requestBody = null;
                    objLog = null;
                }
            }








            public string POSTDatabyKeyHeaderMOBOTP(string lstrHTTPMethod, string URL, string body, string ContentType)
            {
                Log objLog = new Log();
                string lstrOut = string.Empty;
                Dictionary<string, string> requestBody = new Dictionary<string, string>();
                string lstrPostData = string.Empty;
                try
                {
                    ServicePointManager.Expect100Continue = true;
                    ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12
                           | SecurityProtocolType.Tls11
                           | SecurityProtocolType.Ssl3
                           | (SecurityProtocolType)3072;

                    HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(URL);
                    httpWebRequest.Timeout = 60 * 1000;
                    httpWebRequest.ContentType = ContentType;// "application/json";
                    httpWebRequest.Method = lstrHTTPMethod;

                    if (!string.IsNullOrWhiteSpace(body))
                    {
                        //requestBody.Add("Request", crypto.AES_ENCRYPT(body, ConfigurationManager.AppSettings["APIAESKEY"]));
                        //lstrPostData = JsonConvert.SerializeObject(requestBody);
                        // lstrPostData = crypto.AES_ENCRYPT(body, ConfigurationManager.AppSettings["APIAESKEY"]);
                        lstrPostData = body;
                    }
                    else
                    {
                        lstrPostData = body;
                    }


                    byte[] bytes = Encoding.UTF8.GetBytes(lstrPostData);
                    httpWebRequest.Headers.Add("TENANT", "TCNXTIGN");
                    httpWebRequest.Headers.Add("partnerId", "TCNXTIGN");
                    httpWebRequest.Headers.Add("partnerToken", "Basic VENOWFRJR04=");
                    objLog.WriteAppLog(URL + ":" + lstrPostData + ":" + ContentType + ":", lstrFolderName);

                    httpWebRequest.ProtocolVersion = HttpVersion.Version10;


                    using (System.IO.Stream sendStream = httpWebRequest.GetRequestStream())
                    {
                        sendStream.Write(bytes, 0, bytes.Length);
                        sendStream.Close();
                    }

                    HttpWebResponse myHttpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                    if (myHttpWebResponse.StatusCode == HttpStatusCode.OK)
                    {
                        //lstrOut = myHttpWebResponse.StatusDescription;
                    }

                    //System.Net.WebResponse res = httpWebRequest.GetResponse();
                    System.IO.Stream ReceiveStream = myHttpWebResponse.GetResponseStream();
                    using (System.IO.StreamReader sr = new System.IO.StreamReader(ReceiveStream, Encoding.UTF8))
                    {
                        lstrOut = sr.ReadToEnd();
                    }
                    return lstrOut;
                }
                catch (WebException ex)
                {
                    //using (WebResponse response = ex.Response)
                    //{
                    //    HttpWebResponse httpResponse = (HttpWebResponse)response;
                    //    using (Stream data = response.GetResponseStream())
                    //    {
                    //        using (var reader = new StreamReader(data))
                    //        {
                    //            lstrOut = reader.ReadToEnd();
                    //            objLog.WriteErrorLog("POSTData Web exception :" + lstrOut, lstrFolderName);
                    //        }
                    //    }
                    //}
                    objLog.WriteErrorLog("POSTData : Web exception :" + ex.ToString(), lstrFolderName);
                    lstrOut = "";
                    return lstrOut;
                }
                catch (Exception ex)
                {
                    objLog.WriteErrorLog("POSTData :" + ex.ToString(), lstrFolderName);
                    lstrOut = "";
                    return lstrOut;
                }
                finally
                {
                    requestBody = null;
                    objLog = null;
                }
            }
            public string POSTDatabyKeyHeaderUserReg(string lstrHTTPMethod, string URL, string body, string ContentType)
            {
                Log objLog = new Log();
                string lstrOut = string.Empty;
                Dictionary<string, string> requestBody = new Dictionary<string, string>();
                string lstrPostData = string.Empty;
                try
                {
                    ServicePointManager.Expect100Continue = true;
                    ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12
                           | SecurityProtocolType.Tls11
                           | SecurityProtocolType.Ssl3
                           | (SecurityProtocolType)3072;

                    HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(URL);
                    httpWebRequest.Timeout = 60 * 1000;
                    httpWebRequest.ContentType = ContentType;// "application/json";
                    httpWebRequest.Method = lstrHTTPMethod;

                    if (!string.IsNullOrWhiteSpace(body))
                    {
                        //requestBody.Add("Request", crypto.AES_ENCRYPT(body, ConfigurationManager.AppSettings["APIAESKEY"]));
                        //lstrPostData = JsonConvert.SerializeObject(requestBody);
                        // lstrPostData = crypto.AES_ENCRYPT(body, ConfigurationManager.AppSettings["APIAESKEY"]);
                        lstrPostData = body;
                    }
                    else
                    {
                        lstrPostData = body;
                    }


                    byte[] bytes = Encoding.UTF8.GetBytes(lstrPostData);
                    httpWebRequest.Headers.Add("TENANT", "TCNXTIGN");
                    httpWebRequest.Headers.Add("partnerId", "TCNXTIGN");
                    httpWebRequest.Headers.Add("partnerToken", "Basic VENOWFRJR04=");
                    objLog.WriteAppLog(URL + ":" + lstrPostData + ":" + ContentType + ":", lstrFolderName);

                    httpWebRequest.ProtocolVersion = HttpVersion.Version10;


                    using (System.IO.Stream sendStream = httpWebRequest.GetRequestStream())
                    {
                        sendStream.Write(bytes, 0, bytes.Length);
                        sendStream.Close();
                    }

                    HttpWebResponse myHttpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                    if (myHttpWebResponse.StatusCode == HttpStatusCode.OK)
                    {
                        //lstrOut = myHttpWebResponse.StatusDescription;
                    }

                    //System.Net.WebResponse res = httpWebRequest.GetResponse();
                    System.IO.Stream ReceiveStream = myHttpWebResponse.GetResponseStream();
                    using (System.IO.StreamReader sr = new System.IO.StreamReader(ReceiveStream, Encoding.UTF8))
                    {
                        lstrOut = sr.ReadToEnd();
                    }
                    return lstrOut;
                }
                catch (WebException ex)
                {
                    //using (WebResponse response = ex.Response)
                    //{
                    //    HttpWebResponse httpResponse = (HttpWebResponse)response;
                    //    using (Stream data = response.GetResponseStream())
                    //    {
                    //        using (var reader = new StreamReader(data))
                    //        {
                    //            lstrOut = reader.ReadToEnd();
                    //            objLog.WriteErrorLog("POSTData Web exception :" + lstrOut, lstrFolderName);
                    //        }
                    //    }
                    //}
                    objLog.WriteErrorLog("POSTData : Web exception :" + ex.ToString(), lstrFolderName);
                    lstrOut = "";
                    return lstrOut;
                }
                catch (Exception ex)
                {
                    objLog.WriteErrorLog("POSTData :" + ex.ToString(), lstrFolderName);
                    lstrOut = "";
                    return lstrOut;
                }
                finally
                {
                    requestBody = null;
                    objLog = null;
                }
            }
            //public string POSTDatatoAadharURL(string URL, string body, string ContentType)
            //{
            //    Log objLog = new Log();
            //    string lstrOut = string.Empty;
            //    try
            //    {
            //        //ServicePointManager.Expect100Continue = true;
            //        //ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

            //        ServicePointManager.Expect100Continue = true;
            //        ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls
            //               | SecurityProtocolType.Tls11
            //               | SecurityProtocolType.Tls12
            //               | SecurityProtocolType.Ssl3
            //               | (SecurityProtocolType)3072;


            //        // Skip validation of SSL/TLS certificate
            //        // ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };


            //        HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(URL);
            //        httpWebRequest.Timeout = 40 * 1000;
            //        httpWebRequest.ContentType = ContentType;// "application/json";
            //        httpWebRequest.Method = "POST";
            //        byte[] bytes = Encoding.UTF8.GetBytes(body);

            //        httpWebRequest.Headers.Add("api-key", "FJVTS74-RAAM91G-P2Y0DHG-3Z1QGW9");
            //        httpWebRequest.Headers.Add("app-id", "62c7df712d4a96001d8dfb23");
            //        //// Use the X509Store class to get a handle to the local certificate stores. "My" is the "Personal" store.
            //        //X509Store store = new X509Store(StoreName.My, StoreLocation.LocalMachine);

            //        //// Open the store to be able to read from it.
            //        //store.Open(OpenFlags.ReadOnly);

            //        //// Use the X509Certificate2Collection class to get a list of certificates that match our criteria (in this case, we should only pull back one).
            //        //X509Certificate2Collection collection = store.Certificates.Find(X509FindType.FindBySubjectName, "axis.p12", true);

            //        //// Associate the certificates with the request
            //        //httpWebRequest.ClientCertificates = collection;

            //        //string AxisCertificate = ConfigurationManager.AppSettings["CertName"];
            //        //string lstrPassword = "123456";
            //        //X509Certificate2 privateCert1 = new X509Certificate2(AxisCertificate, lstrPassword);
            //        //httpWebRequest.ClientCertificates.Add(privateCert1);

            //        //var certificate = new X509Certificate2(AxisCertificate, lstrPassword, X509KeyStorageFlags.MachineKeySet | X509KeyStorageFlags.PersistKeySet | X509KeyStorageFlags.Exportable);
            //        //httpWebRequest.ClientCertificates.Add(certificate);
            //        //X509Certificate2 privateCert = new X509Certificate2(AxisCertificate, lstrPassword, X509KeyStorageFlags.Exportable);
            //        //httpWebRequest.ClientCertificates.Add(privateCert);

            //        httpWebRequest.ProtocolVersion = HttpVersion.Version10;


            //        using (System.IO.Stream sendStream = httpWebRequest.GetRequestStream())
            //        {
            //            sendStream.Write(bytes, 0, bytes.Length);
            //            sendStream.Close();
            //        }

            //        HttpWebResponse myHttpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            //        if (myHttpWebResponse.StatusCode == HttpStatusCode.OK)
            //        {
            //            lstrOut = myHttpWebResponse.StatusDescription;
            //        }
            //        System.IO.Stream ReceiveStream = myHttpWebResponse.GetResponseStream();
            //        using (System.IO.StreamReader sr = new System.IO.StreamReader(ReceiveStream, Encoding.UTF8))
            //        {
            //            lstrOut = sr.ReadToEnd();
            //        }
            //        objLog.WriteErrorLog("SendPOSTData OUT :" + lstrOut, lstrFolderName);
            //        return lstrOut;
            //    }
            //    catch (WebException ex)
            //    {
            //        objLog.WriteErrorLog("SendPOSTData :" + ex.ToString(), lstrFolderName);

            //        using (WebResponse response = ex.Response)
            //        {
            //            HttpWebResponse httpResponse = (HttpWebResponse)response;
            //            using (Stream data = response.GetResponseStream())
            //            using (var reader = new StreamReader(data))
            //            {
            //                lstrOut = reader.ReadToEnd();
            //                objLog.WriteErrorLog("SendPOSTData1 exception :" + lstrOut, lstrFolderName);
            //            }
            //        }

            //        objLog.WriteErrorLog("SendPOSTData2 :" + ex.ToString(), lstrFolderName);
            //        lstrOut = "";
            //        return lstrOut;
            //    }
            //    catch (Exception ex)
            //    {
            //        objLog.WriteErrorLog("SendPOSTData3 :" + ex.ToString(), lstrFolderName);
            //        lstrOut = "";
            //        return lstrOut;
            //    }
            //}


            public string POSTAadharvalue(string lstrHTTPMethod, string URL, string body, string ContentType)
            {
                Log objLog = new Log();
                string lstrOut = string.Empty;
                Dictionary<string, string> requestBody = new Dictionary<string, string>();
                string lstrPostData = string.Empty;
                try
                {
                    ServicePointManager.Expect100Continue = true;
                    ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12
                           | SecurityProtocolType.Tls11
                           | SecurityProtocolType.Tls12
                           | SecurityProtocolType.Ssl3
                           | (SecurityProtocolType)3072;

                    HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(URL);
                    httpWebRequest.Timeout = 60 * 1000;
                    httpWebRequest.ContentType = ContentType;// "application/json";
                    httpWebRequest.Method = lstrHTTPMethod;

                    //if(!string.IsNullOrWhiteSpace(body))
                    //{
                    //    requestBody.Add("Request", crypto.AES_ENCRYPT(body,COMMON.KEY));
                    //    lstrPostData = JsonConvert.SerializeObject(requestBody);
                    //}
                    //else
                    //{
                    //    lstrPostData = "";
                    //}

                    lstrPostData = body;

                    byte[] bytes = Encoding.UTF8.GetBytes(lstrPostData);
                    httpWebRequest.Headers.Add("api-key", "FJVTS74-RAAM91G-P2Y0DHG-3Z1QGW9");
                    httpWebRequest.Headers.Add("app-id", "62c7df712d4a96001d8dfb23");
                    if (!string.IsNullOrWhiteSpace(body))
                    {
                        //requestBody.Add("Request", crypto.AES_ENCRYPT(body, ConfigurationManager.AppSettings["APIAESKEY"]));
                        //lstrPostData = JsonConvert.SerializeObject(requestBody);
                        // lstrPostData = crypto.AES_ENCRYPT(body, ConfigurationManager.AppSettings["APIAESKEY"]);
                        lstrPostData = body;
                    }
                    else
                    {
                        lstrPostData = body;
                    }
                    objLog.WriteAppLog(URL + ":" + lstrPostData + ":" + ContentType + ":", lstrFolderName);

                    httpWebRequest.ProtocolVersion = HttpVersion.Version10;


                    using (System.IO.Stream sendStream = httpWebRequest.GetRequestStream())
                    {
                        sendStream.Write(bytes, 0, bytes.Length);
                        sendStream.Close();
                    }

                    HttpWebResponse myHttpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                    if (myHttpWebResponse.StatusCode == HttpStatusCode.OK)
                    {
                        //lstrOut = myHttpWebResponse.StatusDescription;
                    }

                    //System.Net.WebResponse res = httpWebRequest.GetResponse();
                    System.IO.Stream ReceiveStream = myHttpWebResponse.GetResponseStream();
                    using (System.IO.StreamReader sr = new System.IO.StreamReader(ReceiveStream, Encoding.UTF8))
                    {
                        lstrOut = sr.ReadToEnd();
                    }
                    return lstrOut;
                }
                catch (WebException ex)
                {
                    using (WebResponse response = ex.Response)
                    {
                        if (response != null)
                        {
                            HttpWebResponse httpResponse = (HttpWebResponse)response;
                            using (Stream data = response.GetResponseStream())
                            {
                                using (var reader = new StreamReader(data))
                                {
                                    lstrOut = reader.ReadToEnd();
                                    objLog.WriteErrorLog("POSTData Web exception :" + lstrOut, lstrFolderName);
                                }
                            }
                        }
                    }
                    objLog.WriteErrorLog("POSTData : Web exception :" + ex.ToString(), lstrFolderName);
                    lstrOut = "";
                    return lstrOut;
                }
                catch (Exception ex)
                {
                    objLog.WriteErrorLog("POSTData :" + ex.ToString(), lstrFolderName);
                    lstrOut = "";
                    return lstrOut;
                }
                finally
                {
                    requestBody = null;
                    objLog = null;
                }
            }
        }

    }


