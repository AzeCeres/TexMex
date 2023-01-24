using System;
using UnityEngine;

namespace OmniDi.Library.Wrappers
{
    ///<summary>
    ///A wrapper struct for unity tags that allows them to be serialized as a drop-down list in the editor.
    ///</summary>
    [Serializable]
    public struct Tag
    {
        [SerializeField]
        private string tag;

        public Tag(string tag)
        {
            this.tag = tag;
        }

        ///<summary>
        ///Gets the tag stored in the struct.
        ///</summary>
        ///<returns>
        ///Returns the tag stored in the struct.
        ///</returns>
        public string GetTag()
        {
            return tag;
        }

        /// <summary>
        /// Compares the current tag with the tag of another GameObject.
        /// </summary>
        /// <param name="other">The GameObject who's tag should be compared with this.</param>
        /// <returns>Returns true if the tags are the same.</returns>
        public bool CompareTag(GameObject other)
        {
            return other.CompareTag(tag);
        }

        /// <summary>
        /// Compares the current tag with the tag of another component.
        /// </summary>
        /// <param name="other">The component who's tag should be compared with this.</param>
        /// <returns>Returns true if the tags are the same.</returns>
        public bool CompareTag(Component other)
        {
            return other.CompareTag(tag);
        }

        /// <summary>
        /// Compares the current tag with another tag.
        /// </summary>
        /// <param name="other">The other tag to compare with this.</param>
        /// <returns>Returns true if both tags are the same.</returns>
        public bool CompareTag(Tag other)
        {
            return tag == other.GetTag();
        }
    }
}