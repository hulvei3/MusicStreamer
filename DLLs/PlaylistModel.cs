using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WMPLib;

namespace StreamerLib
{
    public class PlaylistModel : IWMPPlaylist
    {
        public void appendItem(IWMPMedia pIWMPMedia)
        {
            throw new NotImplementedException();
        }

        public int attributeCount
        {
            get { throw new NotImplementedException(); }
        }

        public void clear()
        {
            throw new NotImplementedException();
        }

        public int count
        {
            get { throw new NotImplementedException(); }
        }

        public string getItemInfo(string bstrName)
        {
            throw new NotImplementedException();
        }

        public IWMPMedia get_Item(int lIndex)
        {
            throw new NotImplementedException();
        }

        public string get_attributeName(int lIndex)
        {
            throw new NotImplementedException();
        }

        public bool get_isIdentical(IWMPPlaylist pIWMPPlaylist)
        {
            throw new NotImplementedException();
        }

        public void insertItem(int lIndex, IWMPMedia pIWMPMedia)
        {
            throw new NotImplementedException();
        }

        public void moveItem(int lIndexOld, int lIndexNew)
        {
            throw new NotImplementedException();
        }

        public string name
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public void removeItem(IWMPMedia pIWMPMedia)
        {
            throw new NotImplementedException();
        }

        public void setItemInfo(string bstrName, string bstrValue)
        {
            throw new NotImplementedException();
        }
    }
}
