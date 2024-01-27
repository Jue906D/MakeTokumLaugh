
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using Luban;
using SimpleJSON;


namespace cfg.gj
{
public partial class TbItem
{
    private readonly System.Collections.Generic.Dictionary<string, Item> _dataMap;
    private readonly System.Collections.Generic.List<Item> _dataList;
    
    public TbItem(JSONNode _buf)
    {
        _dataMap = new System.Collections.Generic.Dictionary<string, Item>();
        _dataList = new System.Collections.Generic.List<Item>();
        
        foreach(JSONNode _ele in _buf.Children)
        {
            Item _v;
            { if(!_ele.IsObject) { throw new SerializationException(); }  _v = Item.DeserializeItem(_ele);  }
            _dataList.Add(_v);
            _dataMap.Add(_v.Id, _v);
        }
    }

    public System.Collections.Generic.Dictionary<string, Item> DataMap => _dataMap;
    public System.Collections.Generic.List<Item> DataList => _dataList;

    public Item GetOrDefault(string key) => _dataMap.TryGetValue(key, out var v) ? v : null;
    public Item Get(string key) => _dataMap[key];
    public Item this[string key] => _dataMap[key];

    public void ResolveRef(Tables tables)
    {
        foreach(var _v in _dataList)
        {
            _v.ResolveRef(tables);
        }
    }

}

}
