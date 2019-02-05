using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{

    [MetadataType(typeof(DataLayer.Entities.ProjectMetaData))]
    public partial class Project
    {

    }
    [MetadataType(typeof(DataLayer.Entities.DownloadFileMetaData))]
    public partial class DownloadFile
    {

    }

    [MetadataType(typeof(DataLayer.Entities.ProjectCategoryMetaData))]
    public partial class ProjectCategory
    {

    }
    [MetadataType(typeof(DataLayer.Entities.ProductGroupMetaData))]
    public partial class ProductGroup
    {

    }

    [MetadataType(typeof(DataLayer.Entities.PostMetaData))]
    public partial class Post
    {

    }


    [MetadataType(typeof(DataLayer.Entities.PostGroupMetaData))]
    public partial class PostGroup
    {

    }
     
    [MetadataType(typeof(DataLayer.Entities.PageContentMetaData))]
    public partial class PageContent
    {

    }

    [MetadataType(typeof(DataLayer.Entities.ContactPMMetaData))]
    public partial class ContactPM
    {

    }
}
