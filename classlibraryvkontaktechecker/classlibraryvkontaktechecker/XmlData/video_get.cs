﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан программой.
//     Исполняемая версия:4.0.30319.530
//
//     Изменения в этом файле могут привести к неправильной работе и будут потеряны в случае
//     повторной генерации кода.
// </auto-generated>
//------------------------------------------------------------------------------

// 
// Этот исходный код был создан с помощью xsd, версия=4.0.30319.1.
// 
namespace ClassLibraryVkontakteChecker.XmlData
{
	using System.Xml.Serialization;
	public class video_get
	{

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
			[System.Xml.Serialization.XmlElementAttribute( "video" , Form = System.Xml.Schema.XmlSchemaForm.Unqualified )]
			public responseVideo[] video;

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
		public partial class responseVideo
		{

			/// <remarks/>
			[System.Xml.Serialization.XmlElementAttribute( Form = System.Xml.Schema.XmlSchemaForm.Unqualified )]
			public string vid;

			/// <remarks/>
			[System.Xml.Serialization.XmlElementAttribute( Form = System.Xml.Schema.XmlSchemaForm.Unqualified )]
			public string owner_id;

			/// <remarks/>
			[System.Xml.Serialization.XmlElementAttribute( Form = System.Xml.Schema.XmlSchemaForm.Unqualified )]
			public string title;

			/// <remarks/>
			[System.Xml.Serialization.XmlElementAttribute( Form = System.Xml.Schema.XmlSchemaForm.Unqualified )]
			public string description;

			/// <remarks/>
			[System.Xml.Serialization.XmlElementAttribute( Form = System.Xml.Schema.XmlSchemaForm.Unqualified )]
			public string duration;

			/// <remarks/>
			[System.Xml.Serialization.XmlElementAttribute( Form = System.Xml.Schema.XmlSchemaForm.Unqualified )]
			public string link;

			/// <remarks/>
			[System.Xml.Serialization.XmlElementAttribute( Form = System.Xml.Schema.XmlSchemaForm.Unqualified )]
			public string image;

			/// <remarks/>
			[System.Xml.Serialization.XmlElementAttribute( Form = System.Xml.Schema.XmlSchemaForm.Unqualified )]
			public string image_medium;

			/// <remarks/>
			[System.Xml.Serialization.XmlElementAttribute( Form = System.Xml.Schema.XmlSchemaForm.Unqualified )]
			public string date;

			/// <remarks/>
			[System.Xml.Serialization.XmlElementAttribute( Form = System.Xml.Schema.XmlSchemaForm.Unqualified )]
			public string views;

			/// <remarks/>
			[System.Xml.Serialization.XmlElementAttribute( Form = System.Xml.Schema.XmlSchemaForm.Unqualified )]
			public string player;
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