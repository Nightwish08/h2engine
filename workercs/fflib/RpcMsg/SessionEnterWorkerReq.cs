/**
 * Autogenerated by Thrift Compiler (0.9.1)
 *
 * DO NOT EDIT UNLESS YOU ARE SURE THAT YOU KNOW WHAT YOU ARE DOING
 *  @generated
 */
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.IO;
using Thrift;
using Thrift.Collections;
using System.Runtime.Serialization;
using Thrift.Protocol;
using Thrift.Transport;

namespace ff
{

  #if !SILVERLIGHT
  [Serializable]
  #endif
  public partial class SessionEnterWorkerReq : TBase
  {
    private long _session_id;
    private string _session_ip;
    private string _from_gate;
    private string _from_worker;
    private string _to_worker;
    private string _extra_data;

    public long Session_id
    {
      get
      {
        return _session_id;
      }
      set
      {
        __isset.session_id = true;
        this._session_id = value;
      }
    }

    public string Session_ip
    {
      get
      {
        return _session_ip;
      }
      set
      {
        __isset.session_ip = true;
        this._session_ip = value;
      }
    }

    public string From_gate
    {
      get
      {
        return _from_gate;
      }
      set
      {
        __isset.from_gate = true;
        this._from_gate = value;
      }
    }

    public string From_worker
    {
      get
      {
        return _from_worker;
      }
      set
      {
        __isset.from_worker = true;
        this._from_worker = value;
      }
    }

    public string To_worker
    {
      get
      {
        return _to_worker;
      }
      set
      {
        __isset.to_worker = true;
        this._to_worker = value;
      }
    }

    public string Extra_data
    {
      get
      {
        return _extra_data;
      }
      set
      {
        __isset.extra_data = true;
        this._extra_data = value;
      }
    }


    public Isset __isset;
    #if !SILVERLIGHT
    [Serializable]
    #endif
    public struct Isset {
      public bool session_id;
      public bool session_ip;
      public bool from_gate;
      public bool from_worker;
      public bool to_worker;
      public bool extra_data;
    }

    public SessionEnterWorkerReq() {
    }

    public void Read (TProtocol iprot)
    {
      TField field;
      iprot.ReadStructBegin();
      while (true)
      {
        field = iprot.ReadFieldBegin();
        if (field.Type == TType.Stop) { 
          break;
        }
        switch (field.ID)
        {
          case 1:
            if (field.Type == TType.I64) {
              Session_id = iprot.ReadI64();
            } else { 
              TProtocolUtil.Skip(iprot, field.Type);
            }
            break;
          case 2:
            if (field.Type == TType.String) {
              Session_ip = iprot.ReadString();
            } else { 
              TProtocolUtil.Skip(iprot, field.Type);
            }
            break;
          case 3:
            if (field.Type == TType.String) {
              From_gate = iprot.ReadString();
            } else { 
              TProtocolUtil.Skip(iprot, field.Type);
            }
            break;
          case 4:
            if (field.Type == TType.String) {
              From_worker = iprot.ReadString();
            } else { 
              TProtocolUtil.Skip(iprot, field.Type);
            }
            break;
          case 5:
            if (field.Type == TType.String) {
              To_worker = iprot.ReadString();
            } else { 
              TProtocolUtil.Skip(iprot, field.Type);
            }
            break;
          case 6:
            if (field.Type == TType.String) {
              Extra_data = iprot.ReadString();
            } else { 
              TProtocolUtil.Skip(iprot, field.Type);
            }
            break;
          default: 
            TProtocolUtil.Skip(iprot, field.Type);
            break;
        }
        iprot.ReadFieldEnd();
      }
      iprot.ReadStructEnd();
    }

    public void Write(TProtocol oprot) {
      TStruct struc = new TStruct("SessionEnterWorkerReq");
      oprot.WriteStructBegin(struc);
      TField field = new TField();
      if (__isset.session_id) {
        field.Name = "session_id";
        field.Type = TType.I64;
        field.ID = 1;
        oprot.WriteFieldBegin(field);
        oprot.WriteI64(Session_id);
        oprot.WriteFieldEnd();
      }
      if (Session_ip != null && __isset.session_ip) {
        field.Name = "session_ip";
        field.Type = TType.String;
        field.ID = 2;
        oprot.WriteFieldBegin(field);
        oprot.WriteString(Session_ip);
        oprot.WriteFieldEnd();
      }
      if (From_gate != null && __isset.from_gate) {
        field.Name = "from_gate";
        field.Type = TType.String;
        field.ID = 3;
        oprot.WriteFieldBegin(field);
        oprot.WriteString(From_gate);
        oprot.WriteFieldEnd();
      }
      if (From_worker != null && __isset.from_worker) {
        field.Name = "from_worker";
        field.Type = TType.String;
        field.ID = 4;
        oprot.WriteFieldBegin(field);
        oprot.WriteString(From_worker);
        oprot.WriteFieldEnd();
      }
      if (To_worker != null && __isset.to_worker) {
        field.Name = "to_worker";
        field.Type = TType.String;
        field.ID = 5;
        oprot.WriteFieldBegin(field);
        oprot.WriteString(To_worker);
        oprot.WriteFieldEnd();
      }
      if (Extra_data != null && __isset.extra_data) {
        field.Name = "extra_data";
        field.Type = TType.String;
        field.ID = 6;
        oprot.WriteFieldBegin(field);
        oprot.WriteString(Extra_data);
        oprot.WriteFieldEnd();
      }
      oprot.WriteFieldStop();
      oprot.WriteStructEnd();
    }

    public override string ToString() {
      StringBuilder sb = new StringBuilder("SessionEnterWorkerReq(");
      sb.Append("Session_id: ");
      sb.Append(Session_id);
      sb.Append(",Session_ip: ");
      sb.Append(Session_ip);
      sb.Append(",From_gate: ");
      sb.Append(From_gate);
      sb.Append(",From_worker: ");
      sb.Append(From_worker);
      sb.Append(",To_worker: ");
      sb.Append(To_worker);
      sb.Append(",Extra_data: ");
      sb.Append(Extra_data);
      sb.Append(")");
      return sb.ToString();
    }

  }

}
