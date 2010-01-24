﻿using N2.Integrity;
using N2.Details;
using N2.Persistence;
using N2.Installation;

namespace N2.Edit.FileSystem.Items
{
	[PageDefinition("Directory",
		InstallerVisibility = InstallerHint.NeverRootOrStartPage,
        IconUrl = "~/N2/Resources/Img/ico/png/folder.png")]
    [RestrictParents(typeof(AbstractDirectory))]
    [WithEditableName]
	[N2.Web.Template("info", "~/N2/Content/FileSystem/Directory.aspx")]
    public class Directory : AbstractDirectory, IActiveContent
    {
        public override void AddTo(ContentItem newParent)
        {
            if (newParent is AbstractDirectory)
            {
				AbstractDirectory dir = EnsureDirectory(newParent);

				string to = Combine(dir.Url, Name);
				if (FileSystem.FileExists(to))
					throw new NameOccupiedException(this, dir);

				if(FileSystem.DirectoryExists(Url))
					FileSystem.MoveDirectory(Url, to);
				else
					FileSystem.CreateDirectory(to);
            	
				Parent = newParent;
            }
            else if(newParent != null)
            {
                new N2Exception(newParent + " is not a Directory. AddTo only works on directories.");
            }
		}

		#region IActiveContent Members

		public void Save()
        {
			if(!FileSystem.DirectoryExists(Url))
				FileSystem.CreateDirectory(Url);
        }

        public void Delete()
        {
			FileSystem.DeleteDirectory(Url);
        }

        public void MoveTo(ContentItem destination)
        {
			AbstractDirectory d = EnsureDirectory(destination);

			string to = Combine(d.Url, Name);
			if (FileSystem.FileExists(to))
				throw new NameOccupiedException(this, d);

			FileSystem.MoveDirectory(Url, to);
        }

        public ContentItem CopyTo(ContentItem destination)
        {
            AbstractDirectory d = AbstractDirectory.EnsureDirectory(destination);

			string to = Combine(d.Url, Name);
			if (FileSystem.FileExists(to))
				throw new NameOccupiedException(this, d);

			FileSystem.CreateDirectory(to);
        	Directory copy = CreateDirectory(FileSystem.GetDirectory(to));

			foreach (File f in GetFiles())
				f.CopyTo(copy);

			foreach (Directory childDir in GetDirectories())
				childDir.CopyTo(copy);

        	return copy;
        }

        #endregion
    }
}