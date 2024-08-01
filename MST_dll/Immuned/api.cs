using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Net;
using System.Runtime.Serialization;
using System.Security.Cryptography;
using System.Text;
using UnityEngine;

namespace Immuned
{
	// Token: 0x0200000F RID: 15
	public class api : MonoBehaviour
	{
		// Token: 0x06000059 RID: 89 RVA: 0x00002B16 File Offset: 0x00000F16
		private IEnumerator Error_ApplicationNotSetupCorrectly()
		{
			api.<Error_ApplicationNotSetupCorrectly>d__4 <Error_ApplicationNotSetupCorrectly>d__ = new api.<Error_ApplicationNotSetupCorrectly>d__4(0);
			<Error_ApplicationNotSetupCorrectly>d__.<>4__this = this;
			return <Error_ApplicationNotSetupCorrectly>d__;
		}

		// Token: 0x0600005A RID: 90 RVA: 0x000077E0 File Offset: 0x00005BE0
		public api(string name, string ownerid, string secret, string version)
		{
			if (string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(ownerid) || string.IsNullOrWhiteSpace(secret) || string.IsNullOrWhiteSpace(version))
			{
				base.StartCoroutine(this.Error_ApplicationNotSetupCorrectly());
			}
			this.api_name = name;
			this.ownerid = ownerid;
			this.secret = secret;
			this.version = version;
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x0600005B RID: 91 RVA: 0x00002B25 File Offset: 0x00000F25
		// (set) Token: 0x0600005C RID: 92 RVA: 0x00002B2D File Offset: 0x00000F2D
		public bool initialized { get; private set; }

		// Token: 0x0600005D RID: 93 RVA: 0x00002B36 File Offset: 0x00000F36
		private IEnumerator Error_ApplicatonNotFound()
		{
			api.<Error_ApplicatonNotFound>d__17 <Error_ApplicatonNotFound>d__ = new api.<Error_ApplicatonNotFound>d__17(0);
			<Error_ApplicatonNotFound>d__.<>4__this = this;
			return <Error_ApplicatonNotFound>d__;
		}

		// Token: 0x0600005E RID: 94 RVA: 0x00007874 File Offset: 0x00005C74
		public void init()
		{
			this.enckey = encryption.sha256(encryption.iv_key());
			string text = encryption.sha256(encryption.iv_key());
			NameValueCollection nameValueCollection = new NameValueCollection();
			nameValueCollection["type"] = encryption.byte_arr_to_str(Encoding.Default.GetBytes("init"));
			nameValueCollection["ver"] = encryption.encrypt(this.version, this.secret, text);
			nameValueCollection["hash"] = null;
			nameValueCollection["enckey"] = encryption.encrypt(this.enckey, this.secret, text);
			nameValueCollection["name"] = encryption.byte_arr_to_str(Encoding.Default.GetBytes(this.api_name));
			nameValueCollection["ownerid"] = encryption.byte_arr_to_str(Encoding.Default.GetBytes(this.ownerid));
			nameValueCollection["init_iv"] = text;
			NameValueCollection nameValueCollection2 = nameValueCollection;
			string text2 = api.req(nameValueCollection2);
			if (text2 == "KeyAuth_Invalid")
			{
				base.StartCoroutine(this.Error_ApplicatonNotFound());
			}
			text2 = encryption.decrypt(text2, this.secret, text);
			api.response_structure response_structure = this.response_decoder.string_to_generic<api.response_structure>(text2);
			this.load_response_struct(response_structure);
			if (response_structure.success)
			{
				this.load_app_data(response_structure.appinfo);
				this.sessionid = response_structure.sessionid;
				this.initialized = true;
			}
			else if (response_structure.message == "invalidver")
			{
				this.app_data.downloadLink = response_structure.download;
			}
		}

		// Token: 0x0600005F RID: 95 RVA: 0x00002B45 File Offset: 0x00000F45
		private IEnumerator Error_PleaseInitializeFirst()
		{
			api.<Error_PleaseInitializeFirst>d__19 <Error_PleaseInitializeFirst>d__ = new api.<Error_PleaseInitializeFirst>d__19(0);
			<Error_PleaseInitializeFirst>d__.<>4__this = this;
			return <Error_PleaseInitializeFirst>d__;
		}

		// Token: 0x06000060 RID: 96 RVA: 0x000079E4 File Offset: 0x00005DE4
		public void register(string username, string pass, string key)
		{
			if (!this.initialized)
			{
				base.StartCoroutine(this.Error_PleaseInitializeFirst());
			}
			string deviceUniqueIdentifier = SystemInfo.deviceUniqueIdentifier;
			string text = encryption.sha256(encryption.iv_key());
			NameValueCollection nameValueCollection = new NameValueCollection();
			nameValueCollection["type"] = encryption.byte_arr_to_str(Encoding.Default.GetBytes("register"));
			nameValueCollection["username"] = encryption.encrypt(username, this.enckey, text);
			nameValueCollection["pass"] = encryption.encrypt(pass, this.enckey, text);
			nameValueCollection["key"] = encryption.encrypt(key, this.enckey, text);
			nameValueCollection["hwid"] = encryption.encrypt(deviceUniqueIdentifier, this.enckey, text);
			nameValueCollection["sessionid"] = encryption.byte_arr_to_str(Encoding.Default.GetBytes(this.sessionid));
			nameValueCollection["name"] = encryption.byte_arr_to_str(Encoding.Default.GetBytes(this.api_name));
			nameValueCollection["ownerid"] = encryption.byte_arr_to_str(Encoding.Default.GetBytes(this.ownerid));
			nameValueCollection["init_iv"] = text;
			NameValueCollection nameValueCollection2 = nameValueCollection;
			string text2 = api.req(nameValueCollection2);
			text2 = encryption.decrypt(text2, this.enckey, text);
			api.response_structure response_structure = this.response_decoder.string_to_generic<api.response_structure>(text2);
			this.load_response_struct(response_structure);
			if (response_structure.success)
			{
				this.load_user_data(response_structure.info);
			}
		}

		// Token: 0x06000061 RID: 97 RVA: 0x00007B4C File Offset: 0x00005F4C
		public void login(string username, string pass)
		{
			if (!this.initialized)
			{
				base.StartCoroutine(this.Error_PleaseInitializeFirst());
			}
			string deviceUniqueIdentifier = SystemInfo.deviceUniqueIdentifier;
			string text = encryption.sha256(encryption.iv_key());
			NameValueCollection nameValueCollection = new NameValueCollection();
			nameValueCollection["type"] = encryption.byte_arr_to_str(Encoding.Default.GetBytes("login"));
			nameValueCollection["username"] = encryption.encrypt(username, this.enckey, text);
			nameValueCollection["pass"] = encryption.encrypt(pass, this.enckey, text);
			nameValueCollection["hwid"] = encryption.encrypt(deviceUniqueIdentifier, this.enckey, text);
			nameValueCollection["sessionid"] = encryption.byte_arr_to_str(Encoding.Default.GetBytes(this.sessionid));
			nameValueCollection["name"] = encryption.byte_arr_to_str(Encoding.Default.GetBytes(this.api_name));
			nameValueCollection["ownerid"] = encryption.byte_arr_to_str(Encoding.Default.GetBytes(this.ownerid));
			nameValueCollection["init_iv"] = text;
			NameValueCollection nameValueCollection2 = nameValueCollection;
			string text2 = api.req(nameValueCollection2);
			text2 = encryption.decrypt(text2, this.enckey, text);
			api.response_structure response_structure = this.response_decoder.string_to_generic<api.response_structure>(text2);
			this.load_response_struct(response_structure);
			if (response_structure.success)
			{
				this.load_user_data(response_structure.info);
			}
		}

		// Token: 0x06000062 RID: 98 RVA: 0x00007C9C File Offset: 0x0000609C
		public void upgrade(string username, string key)
		{
			if (!this.initialized)
			{
				base.StartCoroutine(this.Error_PleaseInitializeFirst());
			}
			string deviceUniqueIdentifier = SystemInfo.deviceUniqueIdentifier;
			string text = encryption.sha256(encryption.iv_key());
			NameValueCollection nameValueCollection = new NameValueCollection();
			nameValueCollection["type"] = encryption.byte_arr_to_str(Encoding.Default.GetBytes("upgrade"));
			nameValueCollection["username"] = encryption.encrypt(username, this.enckey, text);
			nameValueCollection["key"] = encryption.encrypt(key, this.enckey, text);
			nameValueCollection["sessionid"] = encryption.byte_arr_to_str(Encoding.Default.GetBytes(this.sessionid));
			nameValueCollection["name"] = encryption.byte_arr_to_str(Encoding.Default.GetBytes(this.api_name));
			nameValueCollection["ownerid"] = encryption.byte_arr_to_str(Encoding.Default.GetBytes(this.ownerid));
			nameValueCollection["init_iv"] = text;
			NameValueCollection nameValueCollection2 = nameValueCollection;
			string text2 = api.req(nameValueCollection2);
			text2 = encryption.decrypt(text2, this.enckey, text);
			api.response_structure response_structure = this.response_decoder.string_to_generic<api.response_structure>(text2);
			response_structure.success = false;
			this.load_response_struct(response_structure);
		}

		// Token: 0x06000063 RID: 99 RVA: 0x00007DC8 File Offset: 0x000061C8
		public void license(string key)
		{
			if (!this.initialized)
			{
				base.StartCoroutine(this.Error_PleaseInitializeFirst());
			}
			string deviceUniqueIdentifier = SystemInfo.deviceUniqueIdentifier;
			string text = encryption.sha256(encryption.iv_key());
			NameValueCollection nameValueCollection = new NameValueCollection();
			nameValueCollection["type"] = encryption.byte_arr_to_str(Encoding.Default.GetBytes("license"));
			nameValueCollection["key"] = encryption.encrypt(key, this.enckey, text);
			nameValueCollection["hwid"] = encryption.encrypt(deviceUniqueIdentifier, this.enckey, text);
			nameValueCollection["sessionid"] = encryption.byte_arr_to_str(Encoding.Default.GetBytes(this.sessionid));
			nameValueCollection["name"] = encryption.byte_arr_to_str(Encoding.Default.GetBytes(this.api_name));
			nameValueCollection["ownerid"] = encryption.byte_arr_to_str(Encoding.Default.GetBytes(this.ownerid));
			nameValueCollection["init_iv"] = text;
			NameValueCollection nameValueCollection2 = nameValueCollection;
			string text2 = api.req(nameValueCollection2);
			text2 = encryption.decrypt(text2, this.enckey, text);
			api.response_structure response_structure = this.response_decoder.string_to_generic<api.response_structure>(text2);
			this.load_response_struct(response_structure);
			if (response_structure.success)
			{
				this.load_user_data(response_structure.info);
			}
		}

		// Token: 0x06000064 RID: 100 RVA: 0x00007F00 File Offset: 0x00006300
		public void check()
		{
			if (!this.initialized)
			{
				base.StartCoroutine(this.Error_PleaseInitializeFirst());
			}
			string text = encryption.sha256(encryption.iv_key());
			NameValueCollection nameValueCollection = new NameValueCollection();
			nameValueCollection["type"] = encryption.byte_arr_to_str(Encoding.Default.GetBytes("check"));
			nameValueCollection["sessionid"] = encryption.byte_arr_to_str(Encoding.Default.GetBytes(this.sessionid));
			nameValueCollection["name"] = encryption.byte_arr_to_str(Encoding.Default.GetBytes(this.api_name));
			nameValueCollection["ownerid"] = encryption.byte_arr_to_str(Encoding.Default.GetBytes(this.ownerid));
			nameValueCollection["init_iv"] = text;
			NameValueCollection nameValueCollection2 = nameValueCollection;
			string text2 = api.req(nameValueCollection2);
			text2 = encryption.decrypt(text2, this.enckey, text);
			api.response_structure response_structure = this.response_decoder.string_to_generic<api.response_structure>(text2);
			this.load_response_struct(response_structure);
		}

		// Token: 0x06000065 RID: 101 RVA: 0x00007FEC File Offset: 0x000063EC
		public void setvar(string var, string data)
		{
			if (!this.initialized)
			{
				base.StartCoroutine(this.Error_PleaseInitializeFirst());
			}
			string text = encryption.sha256(encryption.iv_key());
			NameValueCollection nameValueCollection = new NameValueCollection();
			nameValueCollection["type"] = encryption.byte_arr_to_str(Encoding.Default.GetBytes("setvar"));
			nameValueCollection["var"] = encryption.encrypt(var, this.enckey, text);
			nameValueCollection["data"] = encryption.encrypt(data, this.enckey, text);
			nameValueCollection["sessionid"] = encryption.byte_arr_to_str(Encoding.Default.GetBytes(this.sessionid));
			nameValueCollection["name"] = encryption.byte_arr_to_str(Encoding.Default.GetBytes(this.api_name));
			nameValueCollection["ownerid"] = encryption.byte_arr_to_str(Encoding.Default.GetBytes(this.ownerid));
			nameValueCollection["init_iv"] = text;
			NameValueCollection nameValueCollection2 = nameValueCollection;
			string text2 = api.req(nameValueCollection2);
			text2 = encryption.decrypt(text2, this.enckey, text);
			api.response_structure response_structure = this.response_decoder.string_to_generic<api.response_structure>(text2);
			this.load_response_struct(response_structure);
		}

		// Token: 0x06000066 RID: 102 RVA: 0x00008108 File Offset: 0x00006508
		public string getvar(string var)
		{
			if (!this.initialized)
			{
				base.StartCoroutine(this.Error_PleaseInitializeFirst());
			}
			string text = encryption.sha256(encryption.iv_key());
			NameValueCollection nameValueCollection = new NameValueCollection();
			nameValueCollection["type"] = encryption.byte_arr_to_str(Encoding.Default.GetBytes("getvar"));
			nameValueCollection["var"] = encryption.encrypt(var, this.enckey, text);
			nameValueCollection["sessionid"] = encryption.byte_arr_to_str(Encoding.Default.GetBytes(this.sessionid));
			nameValueCollection["name"] = encryption.byte_arr_to_str(Encoding.Default.GetBytes(this.api_name));
			nameValueCollection["ownerid"] = encryption.byte_arr_to_str(Encoding.Default.GetBytes(this.ownerid));
			nameValueCollection["init_iv"] = text;
			NameValueCollection nameValueCollection2 = nameValueCollection;
			string text2 = api.req(nameValueCollection2);
			text2 = encryption.decrypt(text2, this.enckey, text);
			api.response_structure response_structure = this.response_decoder.string_to_generic<api.response_structure>(text2);
			this.load_response_struct(response_structure);
			string text3;
			if (response_structure.success)
			{
				text3 = response_structure.response;
			}
			else
			{
				text3 = null;
			}
			return text3;
		}

		// Token: 0x06000067 RID: 103 RVA: 0x00008224 File Offset: 0x00006624
		public void ban()
		{
			if (!this.initialized)
			{
				base.StartCoroutine(this.Error_PleaseInitializeFirst());
			}
			string text = encryption.sha256(encryption.iv_key());
			NameValueCollection nameValueCollection = new NameValueCollection();
			nameValueCollection["type"] = encryption.byte_arr_to_str(Encoding.Default.GetBytes("ban"));
			nameValueCollection["sessionid"] = encryption.byte_arr_to_str(Encoding.Default.GetBytes(this.sessionid));
			nameValueCollection["name"] = encryption.byte_arr_to_str(Encoding.Default.GetBytes(this.api_name));
			nameValueCollection["ownerid"] = encryption.byte_arr_to_str(Encoding.Default.GetBytes(this.ownerid));
			nameValueCollection["init_iv"] = text;
			NameValueCollection nameValueCollection2 = nameValueCollection;
			string text2 = api.req(nameValueCollection2);
			text2 = encryption.decrypt(text2, this.enckey, text);
			api.response_structure response_structure = this.response_decoder.string_to_generic<api.response_structure>(text2);
			this.load_response_struct(response_structure);
		}

		// Token: 0x06000068 RID: 104 RVA: 0x00008310 File Offset: 0x00006710
		public string var(string varid)
		{
			if (!this.initialized)
			{
				base.StartCoroutine(this.Error_PleaseInitializeFirst());
			}
			string deviceUniqueIdentifier = SystemInfo.deviceUniqueIdentifier;
			string text = encryption.sha256(encryption.iv_key());
			NameValueCollection nameValueCollection = new NameValueCollection();
			nameValueCollection["type"] = encryption.byte_arr_to_str(Encoding.Default.GetBytes("var"));
			nameValueCollection["varid"] = encryption.encrypt(varid, this.enckey, text);
			nameValueCollection["sessionid"] = encryption.byte_arr_to_str(Encoding.Default.GetBytes(this.sessionid));
			nameValueCollection["name"] = encryption.byte_arr_to_str(Encoding.Default.GetBytes(this.api_name));
			nameValueCollection["ownerid"] = encryption.byte_arr_to_str(Encoding.Default.GetBytes(this.ownerid));
			nameValueCollection["init_iv"] = text;
			NameValueCollection nameValueCollection2 = nameValueCollection;
			string text2 = api.req(nameValueCollection2);
			text2 = encryption.decrypt(text2, this.enckey, text);
			api.response_structure response_structure = this.response_decoder.string_to_generic<api.response_structure>(text2);
			this.load_response_struct(response_structure);
			string text3;
			if (response_structure.success)
			{
				text3 = response_structure.message;
			}
			else
			{
				text3 = null;
			}
			return text3;
		}

		// Token: 0x06000069 RID: 105 RVA: 0x00008434 File Offset: 0x00006834
		public List<api.users> fetchOnline()
		{
			if (!this.initialized)
			{
				base.StartCoroutine(this.Error_PleaseInitializeFirst());
			}
			string text = encryption.sha256(encryption.iv_key());
			NameValueCollection nameValueCollection = new NameValueCollection();
			nameValueCollection["type"] = encryption.byte_arr_to_str(Encoding.Default.GetBytes("fetchOnline"));
			nameValueCollection["sessionid"] = encryption.byte_arr_to_str(Encoding.Default.GetBytes(this.sessionid));
			nameValueCollection["name"] = encryption.byte_arr_to_str(Encoding.Default.GetBytes(this.api_name));
			nameValueCollection["ownerid"] = encryption.byte_arr_to_str(Encoding.Default.GetBytes(this.ownerid));
			nameValueCollection["init_iv"] = text;
			NameValueCollection nameValueCollection2 = nameValueCollection;
			string text2 = api.req(nameValueCollection2);
			text2 = encryption.decrypt(text2, this.enckey, text);
			api.response_structure response_structure = this.response_decoder.string_to_generic<api.response_structure>(text2);
			this.load_response_struct(response_structure);
			List<api.users> list;
			if (response_structure.success)
			{
				list = response_structure.users;
			}
			else
			{
				list = null;
			}
			return list;
		}

		// Token: 0x0600006A RID: 106 RVA: 0x00008538 File Offset: 0x00006938
		public bool checkblack()
		{
			if (!this.initialized)
			{
				base.StartCoroutine(this.Error_PleaseInitializeFirst());
			}
			string deviceUniqueIdentifier = SystemInfo.deviceUniqueIdentifier;
			string text = encryption.sha256(encryption.iv_key());
			NameValueCollection nameValueCollection = new NameValueCollection();
			nameValueCollection["type"] = encryption.byte_arr_to_str(Encoding.Default.GetBytes("checkblacklist"));
			nameValueCollection["hwid"] = encryption.encrypt(deviceUniqueIdentifier, this.enckey, text);
			nameValueCollection["sessionid"] = encryption.byte_arr_to_str(Encoding.Default.GetBytes(this.sessionid));
			nameValueCollection["name"] = encryption.byte_arr_to_str(Encoding.Default.GetBytes(this.api_name));
			nameValueCollection["ownerid"] = encryption.byte_arr_to_str(Encoding.Default.GetBytes(this.ownerid));
			nameValueCollection["init_iv"] = text;
			NameValueCollection nameValueCollection2 = nameValueCollection;
			string text2 = api.req(nameValueCollection2);
			text2 = encryption.decrypt(text2, this.enckey, text);
			api.response_structure response_structure = this.response_decoder.string_to_generic<api.response_structure>(text2);
			this.load_response_struct(response_structure);
			return response_structure.success;
		}

		// Token: 0x0600006B RID: 107 RVA: 0x00008658 File Offset: 0x00006A58
		public string webhook(string webid, string param, string body = "", string conttype = "")
		{
			string text;
			if (!this.initialized)
			{
				base.StartCoroutine(this.Error_PleaseInitializeFirst());
				text = null;
			}
			else
			{
				string text2 = encryption.sha256(encryption.iv_key());
				NameValueCollection nameValueCollection = new NameValueCollection();
				nameValueCollection["type"] = encryption.byte_arr_to_str(Encoding.Default.GetBytes("webhook"));
				nameValueCollection["webid"] = encryption.encrypt(webid, this.enckey, text2);
				nameValueCollection["params"] = encryption.encrypt(param, this.enckey, text2);
				nameValueCollection["body"] = encryption.encrypt(body, this.enckey, text2);
				nameValueCollection["conttype"] = encryption.encrypt(conttype, this.enckey, text2);
				nameValueCollection["sessionid"] = encryption.byte_arr_to_str(Encoding.Default.GetBytes(this.sessionid));
				nameValueCollection["name"] = encryption.byte_arr_to_str(Encoding.Default.GetBytes(this.api_name));
				nameValueCollection["ownerid"] = encryption.byte_arr_to_str(Encoding.Default.GetBytes(this.ownerid));
				nameValueCollection["init_iv"] = text2;
				NameValueCollection nameValueCollection2 = nameValueCollection;
				string text3 = api.req(nameValueCollection2);
				text3 = encryption.decrypt(text3, this.enckey, text2);
				api.response_structure response_structure = this.response_decoder.string_to_generic<api.response_structure>(text3);
				this.load_response_struct(response_structure);
				if (response_structure.success)
				{
					text = response_structure.response;
				}
				else
				{
					text = null;
				}
			}
			return text;
		}

		// Token: 0x0600006C RID: 108 RVA: 0x000087C4 File Offset: 0x00006BC4
		public byte[] download(string fileid)
		{
			if (!this.initialized)
			{
				base.StartCoroutine(this.Error_PleaseInitializeFirst());
			}
			string text = encryption.sha256(encryption.iv_key());
			NameValueCollection nameValueCollection = new NameValueCollection();
			nameValueCollection["type"] = encryption.byte_arr_to_str(Encoding.Default.GetBytes("file"));
			nameValueCollection["fileid"] = encryption.encrypt(fileid, this.enckey, text);
			nameValueCollection["sessionid"] = encryption.byte_arr_to_str(Encoding.Default.GetBytes(this.sessionid));
			nameValueCollection["name"] = encryption.byte_arr_to_str(Encoding.Default.GetBytes(this.api_name));
			nameValueCollection["ownerid"] = encryption.byte_arr_to_str(Encoding.Default.GetBytes(this.ownerid));
			nameValueCollection["init_iv"] = text;
			NameValueCollection nameValueCollection2 = nameValueCollection;
			string text2 = api.req(nameValueCollection2);
			text2 = encryption.decrypt(text2, this.enckey, text);
			api.response_structure response_structure = this.response_decoder.string_to_generic<api.response_structure>(text2);
			this.load_response_struct(response_structure);
			byte[] array;
			if (response_structure.success)
			{
				array = encryption.str_to_byte_arr(response_structure.contents);
			}
			else
			{
				array = null;
			}
			return array;
		}

		// Token: 0x0600006D RID: 109 RVA: 0x000088E4 File Offset: 0x00006CE4
		public void log(string message)
		{
			if (!this.initialized)
			{
				base.StartCoroutine(this.Error_PleaseInitializeFirst());
			}
			string text = encryption.sha256(encryption.iv_key());
			NameValueCollection nameValueCollection = new NameValueCollection();
			nameValueCollection["type"] = encryption.byte_arr_to_str(Encoding.Default.GetBytes("log"));
			nameValueCollection["pcuser"] = encryption.encrypt(Environment.UserName, this.enckey, text);
			nameValueCollection["message"] = encryption.encrypt(message, this.enckey, text);
			nameValueCollection["sessionid"] = encryption.byte_arr_to_str(Encoding.Default.GetBytes(this.sessionid));
			nameValueCollection["name"] = encryption.byte_arr_to_str(Encoding.Default.GetBytes(this.api_name));
			nameValueCollection["ownerid"] = encryption.byte_arr_to_str(Encoding.Default.GetBytes(this.ownerid));
			nameValueCollection["init_iv"] = text;
			NameValueCollection nameValueCollection2 = nameValueCollection;
			api.req(nameValueCollection2);
		}

		// Token: 0x0600006E RID: 110 RVA: 0x000089E0 File Offset: 0x00006DE0
		public static string checksum(string filename)
		{
			string text;
			using (MD5 md = MD5.Create())
			{
				using (FileStream fileStream = File.OpenRead(filename))
				{
					byte[] array = md.ComputeHash(fileStream);
					text = BitConverter.ToString(array).Replace("-", "").ToLowerInvariant();
				}
			}
			return text;
		}

		// Token: 0x0600006F RID: 111 RVA: 0x00008A58 File Offset: 0x00006E58
		private static string req(NameValueCollection post_data)
		{
			string text;
			try
			{
				using (WebClient webClient = new WebClient())
				{
					byte[] array = webClient.UploadValues("https://www.bypassauth.win/api/1.0", post_data);
					text = Encoding.Default.GetString(array);
				}
			}
			catch (WebException ex)
			{
				HttpWebResponse httpWebResponse = (HttpWebResponse)ex.Response;
				HttpStatusCode statusCode = httpWebResponse.StatusCode;
				HttpStatusCode httpStatusCode = statusCode;
				if (httpStatusCode != (HttpStatusCode)429)
				{
					Debug.LogError("Connection failed. Please try again");
					Application.Quit();
					text = "";
				}
				else
				{
					Debug.LogError("You're connecting too fast. Please slow down your requests and try again");
					Application.Quit();
					text = "";
				}
			}
			return text;
		}

		// Token: 0x06000070 RID: 112 RVA: 0x00008B00 File Offset: 0x00006F00
		private static string req_unenc(NameValueCollection post_data)
		{
			string text;
			try
			{
				using (WebClient webClient = new WebClient())
				{
					byte[] array = webClient.UploadValues("https://www.bypassauth.win/api/1.1/", post_data);
					text = Encoding.Default.GetString(array);
				}
			}
			catch (WebException ex)
			{
				HttpWebResponse httpWebResponse = (HttpWebResponse)ex.Response;
				HttpStatusCode statusCode = httpWebResponse.StatusCode;
				HttpStatusCode httpStatusCode = statusCode;
				if (httpStatusCode != (HttpStatusCode)429)
				{
					Debug.LogError("Connection failed. Please try again");
					Application.Quit();
					text = "";
				}
				else
				{
					text = new WaitForSeconds(3f).ToString();
				}
			}
			return text;
		}

		// Token: 0x06000071 RID: 113 RVA: 0x00008BA4 File Offset: 0x00006FA4
		private void load_app_data(api.app_data_structure data)
		{
			this.app_data.numUsers = data.numUsers;
			this.app_data.numOnlineUsers = data.numOnlineUsers;
			this.app_data.numKeys = data.numKeys;
			this.app_data.version = data.version;
			this.app_data.customerPanelLink = data.customerPanelLink;
		}

		// Token: 0x06000072 RID: 114 RVA: 0x00008C08 File Offset: 0x00007008
		private void load_user_data(api.user_data_structure data)
		{
			this.user_data.username = data.username;
			this.user_data.ip = data.ip;
			this.user_data.hwid = data.hwid;
			this.user_data.createdate = data.createdate;
			this.user_data.lastlogin = data.lastlogin;
			this.user_data.subscriptions = data.subscriptions;
		}

		// Token: 0x06000073 RID: 115 RVA: 0x00008C7C File Offset: 0x0000707C
		public string expirydaysleft()
		{
			if (!this.initialized)
			{
				base.StartCoroutine(this.Error_PleaseInitializeFirst());
			}
			DateTime dateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Local);
			dateTime = dateTime.AddSeconds((double)long.Parse(this.user_data.subscriptions[0].expiry)).ToLocalTime();
			TimeSpan timeSpan = dateTime - DateTime.Now;
			return Convert.ToString(timeSpan.Days.ToString() + " Days " + timeSpan.Hours.ToString() + " Hours Left");
		}

		// Token: 0x06000074 RID: 116 RVA: 0x00002B54 File Offset: 0x00000F54
		private void load_response_struct(api.response_structure data)
		{
			this.response.success = data.success;
			this.response.message = data.message;
		}

		// Token: 0x04000043 RID: 67
		public string api_name;

		// Token: 0x04000044 RID: 68
		public string ownerid;

		// Token: 0x04000045 RID: 69
		public string secret;

		// Token: 0x04000046 RID: 70
		public string version;

		// Token: 0x04000047 RID: 71
		private string sessionid;

		// Token: 0x04000048 RID: 72
		private string enckey;

		// Token: 0x0400004A RID: 74
		public api.app_data_class app_data = new api.app_data_class();

		// Token: 0x0400004B RID: 75
		public api.user_data_class user_data = new api.user_data_class();

		// Token: 0x0400004C RID: 76
		public api.response_class response = new api.response_class();

		// Token: 0x0400004D RID: 77
		private json_wrapper response_decoder = new json_wrapper(new api.response_structure());

		// Token: 0x02000010 RID: 16
		[DataContract]
		private class response_structure
		{
			// Token: 0x17000004 RID: 4
			// (get) Token: 0x06000075 RID: 117 RVA: 0x00002B78 File Offset: 0x00000F78
			// (set) Token: 0x06000076 RID: 118 RVA: 0x00002B80 File Offset: 0x00000F80
			[DataMember]
			public bool success { get; set; }

			// Token: 0x17000005 RID: 5
			// (get) Token: 0x06000077 RID: 119 RVA: 0x00002B89 File Offset: 0x00000F89
			// (set) Token: 0x06000078 RID: 120 RVA: 0x00002B91 File Offset: 0x00000F91
			[DataMember]
			public string sessionid { get; set; }

			// Token: 0x17000006 RID: 6
			// (get) Token: 0x06000079 RID: 121 RVA: 0x00002B9A File Offset: 0x00000F9A
			// (set) Token: 0x0600007A RID: 122 RVA: 0x00002BA2 File Offset: 0x00000FA2
			[DataMember]
			public string contents { get; set; }

			// Token: 0x17000007 RID: 7
			// (get) Token: 0x0600007B RID: 123 RVA: 0x00002BAB File Offset: 0x00000FAB
			// (set) Token: 0x0600007C RID: 124 RVA: 0x00002BB3 File Offset: 0x00000FB3
			[DataMember]
			public string response { get; set; }

			// Token: 0x17000008 RID: 8
			// (get) Token: 0x0600007D RID: 125 RVA: 0x00002BBC File Offset: 0x00000FBC
			// (set) Token: 0x0600007E RID: 126 RVA: 0x00002BC4 File Offset: 0x00000FC4
			[DataMember]
			public string message { get; set; }

			// Token: 0x17000009 RID: 9
			// (get) Token: 0x0600007F RID: 127 RVA: 0x00002BCD File Offset: 0x00000FCD
			// (set) Token: 0x06000080 RID: 128 RVA: 0x00002BD5 File Offset: 0x00000FD5
			[DataMember]
			public string download { get; set; }

			// Token: 0x1700000A RID: 10
			// (get) Token: 0x06000081 RID: 129 RVA: 0x00002BDE File Offset: 0x00000FDE
			// (set) Token: 0x06000082 RID: 130 RVA: 0x00002BE6 File Offset: 0x00000FE6
			[DataMember(IsRequired = false, EmitDefaultValue = false)]
			public api.user_data_structure info { get; set; }

			// Token: 0x1700000B RID: 11
			// (get) Token: 0x06000083 RID: 131 RVA: 0x00002BEF File Offset: 0x00000FEF
			// (set) Token: 0x06000084 RID: 132 RVA: 0x00002BF7 File Offset: 0x00000FF7
			[DataMember(IsRequired = false, EmitDefaultValue = false)]
			public api.app_data_structure appinfo { get; set; }

			// Token: 0x1700000C RID: 12
			// (get) Token: 0x06000085 RID: 133 RVA: 0x00002C00 File Offset: 0x00001000
			// (set) Token: 0x06000086 RID: 134 RVA: 0x00002C08 File Offset: 0x00001008
			[DataMember]
			public List<api.msg> messages { get; set; }

			// Token: 0x1700000D RID: 13
			// (get) Token: 0x06000087 RID: 135 RVA: 0x00002C11 File Offset: 0x00001011
			// (set) Token: 0x06000088 RID: 136 RVA: 0x00002C19 File Offset: 0x00001019
			[DataMember]
			public List<api.users> users { get; set; }
		}

		// Token: 0x02000011 RID: 17
		public class msg
		{
			// Token: 0x1700000E RID: 14
			// (get) Token: 0x0600008A RID: 138 RVA: 0x00002C2A File Offset: 0x0000102A
			// (set) Token: 0x0600008B RID: 139 RVA: 0x00002C32 File Offset: 0x00001032
			public string message { get; set; }

			// Token: 0x1700000F RID: 15
			// (get) Token: 0x0600008C RID: 140 RVA: 0x00002C3B File Offset: 0x0000103B
			// (set) Token: 0x0600008D RID: 141 RVA: 0x00002C43 File Offset: 0x00001043
			public string author { get; set; }

			// Token: 0x17000010 RID: 16
			// (get) Token: 0x0600008E RID: 142 RVA: 0x00002C4C File Offset: 0x0000104C
			// (set) Token: 0x0600008F RID: 143 RVA: 0x00002C54 File Offset: 0x00001054
			public string timestamp { get; set; }
		}

		// Token: 0x02000012 RID: 18
		public class users
		{
			// Token: 0x17000011 RID: 17
			// (get) Token: 0x06000091 RID: 145 RVA: 0x00002C5D File Offset: 0x0000105D
			// (set) Token: 0x06000092 RID: 146 RVA: 0x00002C65 File Offset: 0x00001065
			public string credential { get; set; }
		}

		// Token: 0x02000013 RID: 19
		[DataContract]
		private class user_data_structure
		{
			// Token: 0x17000012 RID: 18
			// (get) Token: 0x06000094 RID: 148 RVA: 0x00002C6E File Offset: 0x0000106E
			// (set) Token: 0x06000095 RID: 149 RVA: 0x00002C76 File Offset: 0x00001076
			[DataMember]
			public string username { get; set; }

			// Token: 0x17000013 RID: 19
			// (get) Token: 0x06000096 RID: 150 RVA: 0x00002C7F File Offset: 0x0000107F
			// (set) Token: 0x06000097 RID: 151 RVA: 0x00002C87 File Offset: 0x00001087
			[DataMember]
			public string ip { get; set; }

			// Token: 0x17000014 RID: 20
			// (get) Token: 0x06000098 RID: 152 RVA: 0x00002C90 File Offset: 0x00001090
			// (set) Token: 0x06000099 RID: 153 RVA: 0x00002C98 File Offset: 0x00001098
			[DataMember]
			public string hwid { get; set; }

			// Token: 0x17000015 RID: 21
			// (get) Token: 0x0600009A RID: 154 RVA: 0x00002CA1 File Offset: 0x000010A1
			// (set) Token: 0x0600009B RID: 155 RVA: 0x00002CA9 File Offset: 0x000010A9
			[DataMember]
			public string createdate { get; set; }

			// Token: 0x17000016 RID: 22
			// (get) Token: 0x0600009C RID: 156 RVA: 0x00002CB2 File Offset: 0x000010B2
			// (set) Token: 0x0600009D RID: 157 RVA: 0x00002CBA File Offset: 0x000010BA
			[DataMember]
			public string lastlogin { get; set; }

			// Token: 0x17000017 RID: 23
			// (get) Token: 0x0600009E RID: 158 RVA: 0x00002CC3 File Offset: 0x000010C3
			// (set) Token: 0x0600009F RID: 159 RVA: 0x00002CCB File Offset: 0x000010CB
			[DataMember]
			public List<api.Data> subscriptions { get; set; }
		}

		// Token: 0x02000014 RID: 20
		[DataContract]
		private class app_data_structure
		{
			// Token: 0x17000018 RID: 24
			// (get) Token: 0x060000A1 RID: 161 RVA: 0x00002CD4 File Offset: 0x000010D4
			// (set) Token: 0x060000A2 RID: 162 RVA: 0x00002CDC File Offset: 0x000010DC
			[DataMember]
			public string numUsers { get; set; }

			// Token: 0x17000019 RID: 25
			// (get) Token: 0x060000A3 RID: 163 RVA: 0x00002CE5 File Offset: 0x000010E5
			// (set) Token: 0x060000A4 RID: 164 RVA: 0x00002CED File Offset: 0x000010ED
			[DataMember]
			public string numOnlineUsers { get; set; }

			// Token: 0x1700001A RID: 26
			// (get) Token: 0x060000A5 RID: 165 RVA: 0x00002CF6 File Offset: 0x000010F6
			// (set) Token: 0x060000A6 RID: 166 RVA: 0x00002CFE File Offset: 0x000010FE
			[DataMember]
			public string numKeys { get; set; }

			// Token: 0x1700001B RID: 27
			// (get) Token: 0x060000A7 RID: 167 RVA: 0x00002D07 File Offset: 0x00001107
			// (set) Token: 0x060000A8 RID: 168 RVA: 0x00002D0F File Offset: 0x0000110F
			[DataMember]
			public string version { get; set; }

			// Token: 0x1700001C RID: 28
			// (get) Token: 0x060000A9 RID: 169 RVA: 0x00002D18 File Offset: 0x00001118
			// (set) Token: 0x060000AA RID: 170 RVA: 0x00002D20 File Offset: 0x00001120
			[DataMember]
			public string customerPanelLink { get; set; }

			// Token: 0x1700001D RID: 29
			// (get) Token: 0x060000AB RID: 171 RVA: 0x00002D29 File Offset: 0x00001129
			// (set) Token: 0x060000AC RID: 172 RVA: 0x00002D31 File Offset: 0x00001131
			[DataMember]
			public string downloadLink { get; set; }
		}

		// Token: 0x02000015 RID: 21
		public class app_data_class
		{
			// Token: 0x1700001E RID: 30
			// (get) Token: 0x060000AE RID: 174 RVA: 0x00002D3A File Offset: 0x0000113A
			// (set) Token: 0x060000AF RID: 175 RVA: 0x00002D42 File Offset: 0x00001142
			public string numUsers { get; set; }

			// Token: 0x1700001F RID: 31
			// (get) Token: 0x060000B0 RID: 176 RVA: 0x00002D4B File Offset: 0x0000114B
			// (set) Token: 0x060000B1 RID: 177 RVA: 0x00002D53 File Offset: 0x00001153
			public string numOnlineUsers { get; set; }

			// Token: 0x17000020 RID: 32
			// (get) Token: 0x060000B2 RID: 178 RVA: 0x00002D5C File Offset: 0x0000115C
			// (set) Token: 0x060000B3 RID: 179 RVA: 0x00002D64 File Offset: 0x00001164
			public string numKeys { get; set; }

			// Token: 0x17000021 RID: 33
			// (get) Token: 0x060000B4 RID: 180 RVA: 0x00002D6D File Offset: 0x0000116D
			// (set) Token: 0x060000B5 RID: 181 RVA: 0x00002D75 File Offset: 0x00001175
			public string version { get; set; }

			// Token: 0x17000022 RID: 34
			// (get) Token: 0x060000B6 RID: 182 RVA: 0x00002D7E File Offset: 0x0000117E
			// (set) Token: 0x060000B7 RID: 183 RVA: 0x00002D86 File Offset: 0x00001186
			public string customerPanelLink { get; set; }

			// Token: 0x17000023 RID: 35
			// (get) Token: 0x060000B8 RID: 184 RVA: 0x00002D8F File Offset: 0x0000118F
			// (set) Token: 0x060000B9 RID: 185 RVA: 0x00002D97 File Offset: 0x00001197
			public string downloadLink { get; set; }
		}

		// Token: 0x02000016 RID: 22
		public class user_data_class
		{
			// Token: 0x17000024 RID: 36
			// (get) Token: 0x060000BB RID: 187 RVA: 0x00002DA0 File Offset: 0x000011A0
			// (set) Token: 0x060000BC RID: 188 RVA: 0x00002DA8 File Offset: 0x000011A8
			public string username { get; set; }

			// Token: 0x17000025 RID: 37
			// (get) Token: 0x060000BD RID: 189 RVA: 0x00002DB1 File Offset: 0x000011B1
			// (set) Token: 0x060000BE RID: 190 RVA: 0x00002DB9 File Offset: 0x000011B9
			public string ip { get; set; }

			// Token: 0x17000026 RID: 38
			// (get) Token: 0x060000BF RID: 191 RVA: 0x00002DC2 File Offset: 0x000011C2
			// (set) Token: 0x060000C0 RID: 192 RVA: 0x00002DCA File Offset: 0x000011CA
			public string hwid { get; set; }

			// Token: 0x17000027 RID: 39
			// (get) Token: 0x060000C1 RID: 193 RVA: 0x00002DD3 File Offset: 0x000011D3
			// (set) Token: 0x060000C2 RID: 194 RVA: 0x00002DDB File Offset: 0x000011DB
			public string createdate { get; set; }

			// Token: 0x17000028 RID: 40
			// (get) Token: 0x060000C3 RID: 195 RVA: 0x00002DE4 File Offset: 0x000011E4
			// (set) Token: 0x060000C4 RID: 196 RVA: 0x00002DEC File Offset: 0x000011EC
			public string lastlogin { get; set; }

			// Token: 0x17000029 RID: 41
			// (get) Token: 0x060000C5 RID: 197 RVA: 0x00002DF5 File Offset: 0x000011F5
			// (set) Token: 0x060000C6 RID: 198 RVA: 0x00002DFD File Offset: 0x000011FD
			public List<api.Data> subscriptions { get; set; }
		}

		// Token: 0x02000017 RID: 23
		public class Data
		{
			// Token: 0x1700002A RID: 42
			// (get) Token: 0x060000C8 RID: 200 RVA: 0x00002E06 File Offset: 0x00001206
			// (set) Token: 0x060000C9 RID: 201 RVA: 0x00002E0E File Offset: 0x0000120E
			public string subscription { get; set; }

			// Token: 0x1700002B RID: 43
			// (get) Token: 0x060000CA RID: 202 RVA: 0x00002E17 File Offset: 0x00001217
			// (set) Token: 0x060000CB RID: 203 RVA: 0x00002E1F File Offset: 0x0000121F
			public string expiry { get; set; }

			// Token: 0x1700002C RID: 44
			// (get) Token: 0x060000CC RID: 204 RVA: 0x00002E28 File Offset: 0x00001228
			// (set) Token: 0x060000CD RID: 205 RVA: 0x00002E30 File Offset: 0x00001230
			public string timeleft { get; set; }
		}

		// Token: 0x02000018 RID: 24
		public class response_class
		{
			// Token: 0x1700002D RID: 45
			// (get) Token: 0x060000CF RID: 207 RVA: 0x00002E39 File Offset: 0x00001239
			// (set) Token: 0x060000D0 RID: 208 RVA: 0x00002E41 File Offset: 0x00001241
			public bool success { get; set; }

			// Token: 0x1700002E RID: 46
			// (get) Token: 0x060000D1 RID: 209 RVA: 0x00002E4A File Offset: 0x0000124A
			// (set) Token: 0x060000D2 RID: 210 RVA: 0x00002E52 File Offset: 0x00001252
			public string message { get; set; }
		}
	}
}
