using System;
using System.Collections.Generic;
using System.Text;

namespace com.ataxlab.alfwm.core.collections
{
    /// <summary>
    /// as per https://social.technet.microsoft.com/wiki/contents/articles/17556.how-to-query-trees-using-linq.aspx
    /// </summary>
    /// <typeparam name="TPayload"></typeparam>
    public class TreeNode<TPayload>
        where TPayload : class, new()
    {
        public string Id { get; set; }
        public TPayload Data { get; set; }
        public List<TreeNode<TPayload>> Children { get; set; }

        public TreeNode(string id, TPayload data)
            : this(id, data, new TreeNode<TPayload>[0])
        { }

        public TreeNode(string node, TPayload data, params TreeNode<TPayload>[] children)
        {
            Id = node;
            Data = data;
            Children = new List<TreeNode<TPayload>>(children);
        }
    }
}
