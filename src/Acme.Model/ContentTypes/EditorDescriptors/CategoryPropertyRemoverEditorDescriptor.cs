namespace Acme.Model.ContentTypes.EditorDescriptors
{
    using System;
    using System.Collections.Generic;
    using EPiServer.Core;
    using EPiServer.Shell.ObjectEditing;
    using EPiServer.Shell.ObjectEditing.EditorDescriptors;

    /// <summary>
    ///     Hides the default category property in edit mode. The property is rarely used.
    /// </summary>
    [EditorDescriptorRegistration(TargetType = typeof(ContentData))]
    public class CategoryPropertyRemoverEditorDescriptor : EditorDescriptor
    {
        public override void ModifyMetadata(ExtendedMetadata metadata, IEnumerable<Attribute> attributes)
        {
            foreach (var modelMetadata in metadata.Properties)
            {
                var property = (ExtendedMetadata)modelMetadata;

                if (property.PropertyName == "icategorizable_category")
                {
                    property.ShowForEdit = false;
                }
            }
        }
    }
}
