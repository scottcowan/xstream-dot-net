using System;
using System.Collections;

namespace Xstream.Core
{   
    public abstract class XStreamBase
    {
        protected IMarshalContext context;
        internal readonly XStreamMarshaller marshaller;
        private XStreamBase(MarshalContext context)
        {
            this.context = context;
            marshaller = new XStreamMarshaller();
            context.Alias("GenericHolder", typeof (GenericObjectHolder));
        }

        public XStreamBase()
            : this(new MarshalContext())
        {
        }

        public XStreamBase(IEqualityComparer equalityComparer)
            : this(new MarshalContext(equalityComparer))
        {
        }

        public bool CaseSensitive
        {
            get { return context.CaseSensitive; }
            set { context.CaseSensitive = value; }
        }

        internal XStreamBase AutoAlias<TypeOfRoot>()
        {
            return AutoAlias(typeof (TypeOfRoot));            
        }

        internal XStreamBase AutoAlias(Type TypeOfRoot)
        {
            string root_namespace = TypeOfRoot.Namespace;
            foreach (Type t in TypeOfRoot.Assembly.GetTypes())
            {
                if (t.Namespace == root_namespace)
                {
                    context.Alias(t.Name, t);
                }
            }
            return this;
        }

        /// <summary>
        /// needed since we cannot serialise a generic object at the source
        /// </summary>
        internal class GenericObjectHolder
        {
            public readonly object Value;

            public GenericObjectHolder(object value)
            {
                Value = value;
            }
        }

        internal XStreamBase AddIgnoreAttribute(Type ignoredAttributeType)
        {
            context.AddIgnoreAttribute(ignoredAttributeType);
            return this;
        }

        internal XStreamBase AddConverter(IConverter converter)
        {
            context.AddConverter(converter);
            return this;
        }

        internal XStreamBase AddCData<T>(string name)
        {
            context.AddCdata(typeof(T), name);
            return this;
        }

        public XStreamBase Alias<T>(string alias)
        {
            context.Alias(alias, typeof(T));
            return this;
        }        

        public virtual T FromXml<T>(string xml) where T : class
        {
            object value = marshaller.FromXml(xml, context);
            if (value is GenericObjectHolder) value = ((GenericObjectHolder)value).Value;
            return value as T;
        }

        /// <summary>
        /// Converts the given object to XML representation.
        /// </summary>
        public virtual string ToXml(object value)
        {
            if (value.GetType().IsGenericType) value = new GenericObjectHolder(value);
            return marshaller.ToXml(value, context);
        }
    }
}