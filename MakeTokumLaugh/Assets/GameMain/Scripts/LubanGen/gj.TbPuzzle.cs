
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
public partial class TbPuzzle
{
    private readonly System.Collections.Generic.Dictionary<int, Puzzle> _dataMap;
    private readonly System.Collections.Generic.List<Puzzle> _dataList;
    
    public TbPuzzle(JSONNode _buf)
    {
        _dataMap = new System.Collections.Generic.Dictionary<int, Puzzle>();
        _dataList = new System.Collections.Generic.List<Puzzle>();
        
        foreach(JSONNode _ele in _buf.Children)
        {
            Puzzle _v;
            { if(!_ele.IsObject) { throw new SerializationException(); }  _v = Puzzle.DeserializePuzzle(_ele);  }
            _dataList.Add(_v);
            _dataMap.Add(_v.Id, _v);
        }
    }

    public System.Collections.Generic.Dictionary<int, Puzzle> DataMap => _dataMap;
    public System.Collections.Generic.List<Puzzle> DataList => _dataList;

    public Puzzle GetOrDefault(int key) => _dataMap.TryGetValue(key, out var v) ? v : null;
    public Puzzle Get(int key) => _dataMap[key];
    public Puzzle this[int key] => _dataMap[key];

    public void ResolveRef(Tables tables)
    {
        foreach(var _v in _dataList)
        {
            _v.ResolveRef(tables);
        }
    }

}

}
