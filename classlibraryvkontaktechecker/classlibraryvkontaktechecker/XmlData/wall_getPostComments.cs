﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан программой.
//     Исполняемая версия:4.0.30319.17379
//
//     Изменения в этом файле могут привести к неправильной работе и будут потеряны в случае
//     повторной генерации кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ClassLibraryVkontakteChecker.XmlData
{
	using System.Xml.Serialization;
	public class wall_getPostComments
	{

		// 
		// Этот исходный код был создан с помощью xsd, версия=4.0.30319.1.
		// 


		/// <remarks/>
		[System.CodeDom.Compiler.GeneratedCodeAttribute( "xsd" , "4.0.30319.1" )]
		[System.SerializableAttribute()]
		[System.Diagnostics.DebuggerStepThroughAttribute()]
		[System.ComponentModel.DesignerCategoryAttribute( "code" )]
		[System.Xml.Serialization.XmlTypeAttribute( AnonymousType = true )]
		[System.Xml.Serialization.XmlRootAttribute( Namespace = "" , IsNullable = false )]
		public partial class response
		{

			/// <remarks/>
			[System.Xml.Serialization.XmlElementAttribute( Form = System.Xml.Schema.XmlSchemaForm.Unqualified )]
			public string count;

			/// <remarks/>
			[System.Xml.Serialization.XmlElementAttribute( "comment" , Form = System.Xml.Schema.XmlSchemaForm.Unqualified )]
			public responseComment[] comment;

			/// <remarks/>
			[System.Xml.Serialization.XmlAttributeAttribute()]
			public string list;
		}

		/// <remarks/>
		[System.CodeDom.Compiler.GeneratedCodeAttribute( "xsd" , "4.0.30319.1" )]
		[System.SerializableAttribute()]
		[System.Diagnostics.DebuggerStepThroughAttribute()]
		[System.ComponentModel.DesignerCategoryAttribute( "code" )]
		[System.Xml.Serialization.XmlTypeAttribute( AnonymousType = true )]
		public partial class responseComment
		{

			/// <remarks/>
			[System.Xml.Serialization.XmlElementAttribute( Form = System.Xml.Schema.XmlSchemaForm.Unqualified )]
			public string cid;

			/// <remarks/>
			[System.Xml.Serialization.XmlElementAttribute( Form = System.Xml.Schema.XmlSchemaForm.Unqualified )]
			public string uid;

			/// <remarks/>
			[System.Xml.Serialization.XmlElementAttribute( Form = System.Xml.Schema.XmlSchemaForm.Unqualified )]
			public string from_id;

			/// <remarks/>
			[System.Xml.Serialization.XmlElementAttribute( Form = System.Xml.Schema.XmlSchemaForm.Unqualified )]
			public string date;

			/// <remarks/>
			[System.Xml.Serialization.XmlElementAttribute( Form = System.Xml.Schema.XmlSchemaForm.Unqualified )]
			public string text;

			/// <remarks/>
			[System.Xml.Serialization.XmlElementAttribute( Form = System.Xml.Schema.XmlSchemaForm.Unqualified )]
			public string reply_to_uid;

			/// <remarks/>
			[System.Xml.Serialization.XmlElementAttribute( Form = System.Xml.Schema.XmlSchemaForm.Unqualified )]
			public string reply_to_cid;

			/// <remarks/>
			[System.Xml.Serialization.XmlElementAttribute( "likes" , Form = System.Xml.Schema.XmlSchemaForm.Unqualified )]
			public responseCommentLikes[] likes;
		}

		/// <remarks/>
		[System.CodeDom.Compiler.GeneratedCodeAttribute( "xsd" , "4.0.30319.1" )]
		[System.SerializableAttribute()]
		[System.Diagnostics.DebuggerStepThroughAttribute()]
		[System.ComponentModel.DesignerCategoryAttribute( "code" )]
		[System.Xml.Serialization.XmlTypeAttribute( AnonymousType = true )]
		public partial class responseCommentLikes
		{

			/// <remarks/>
			[System.Xml.Serialization.XmlElementAttribute( Form = System.Xml.Schema.XmlSchemaForm.Unqualified )]
			public string count;

			/// <remarks/>
			[System.Xml.Serialization.XmlElementAttribute( Form = System.Xml.Schema.XmlSchemaForm.Unqualified )]
			public string user_likes;

			/// <remarks/>
			[System.Xml.Serialization.XmlElementAttribute( Form = System.Xml.Schema.XmlSchemaForm.Unqualified )]
			public string can_like;
		}

		/// <remarks/>
		[System.CodeDom.Compiler.GeneratedCodeAttribute( "xsd" , "4.0.30319.1" )]
		[System.SerializableAttribute()]
		[System.Diagnostics.DebuggerStepThroughAttribute()]
		[System.ComponentModel.DesignerCategoryAttribute( "code" )]
		[System.Xml.Serialization.XmlTypeAttribute( AnonymousType = true )]
		[System.Xml.Serialization.XmlRootAttribute( Namespace = "" , IsNullable = false )]
		public partial class NewDataSet
		{

			/// <remarks/>
			[System.Xml.Serialization.XmlElementAttribute( "response" )]
			public response[] Items;
		}
	}
}