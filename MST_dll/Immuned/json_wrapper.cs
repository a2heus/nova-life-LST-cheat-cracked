using System;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Text;

namespace Immuned
{
	// Token: 0x0200001D RID: 29
	public class json_wrapper
	{
		// Token: 0x060000EE RID: 238 RVA: 0x00002EC5 File Offset: 0x000012C5
		public static bool is_serializable(Type to_check)
		{
			return to_check.IsSerializable || to_check.IsDefined(typeof(DataContractAttribute), true);
		}

		// Token: 0x060000EF RID: 239 RVA: 0x0000912C File Offset: 0x0000752C
		public json_wrapper(object obj_to_work_with)
		{
			this.current_object = obj_to_work_with;
			Type type = this.current_object.GetType();
			this.serializer = new DataContractJsonSerializer(type);
			if (!json_wrapper.is_serializable(type))
			{
				throw new Exception(string.Format("the object {0} isn't a serializable", this.current_object));
			}
		}

		// Token: 0x060000F0 RID: 240 RVA: 0x00009180 File Offset: 0x00007580
		public object string_to_object(string json)
		{
			byte[] bytes = Encoding.Default.GetBytes(json);
			object obj;
			using (MemoryStream memoryStream = new MemoryStream(bytes))
			{
				obj = this.serializer.ReadObject(memoryStream);
			}
			return obj;
		}

		// Token: 0x060000F1 RID: 241 RVA: 0x00002EE3 File Offset: 0x000012E3
		public T string_to_generic<T>(string json)
		{
			return (T)((object)this.string_to_object(json));
		}

		// Token: 0x04000082 RID: 130
		private DataContractJsonSerializer serializer;

		// Token: 0x04000083 RID: 131
		private object current_object;
	}
}
