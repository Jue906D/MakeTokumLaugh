
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using Luban;
using SimpleJSON;

namespace cfg
{
public partial class Tables
{
    public gj.TbPuzzle TbPuzzle {get; }
    public gj.TbItem TbItem {get; }

    public Tables(System.Func<string, JSONNode> loader)
    {
        TbPuzzle = new gj.TbPuzzle(loader("gj_tbpuzzle"));
        TbItem = new gj.TbItem(loader("gj_tbitem"));
        ResolveRef();
    }
    
    private void ResolveRef()
    {
        TbPuzzle.ResolveRef(this);
        TbItem.ResolveRef(this);
    }
}

}
