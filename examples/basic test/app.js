/*
Node 10 and lower
  npm i ffi
  https://www.npmjs.com/package/ffi
  https://github.com/node-ffi/node-ffi
Node 11 and 12
  npm i @saleae/ffi
  https://www.npmjs.com/package/@saleae/ffi
  https://github.com/lxe/node-ffi/tree/node-12
Node 13 and higher
  npm i ffi-napi
  https://www.npmjs.com/package/ffi-napi
  https://github.com/node-ffi-napi/node-ffi-napi
*/

// Import dependencies
const ffi = require("ffi-napi");

// Import math library
const instanceCreatorLibrary = new ffi.Library("./InstanceCreatorLibrary", {
  "LoadAssemblies": [
    "void", []
  ], "GetAssemblyTypes": [
    "string", ["string"]
  ], "CreateInstance": [
    "void", ["string", "string", "string", "string"]
  ], "GetFieldString": [
    "string", ["string", "string"]
  ], "GetFieldInt": [
    "int", ["string", "string"]
  ], "GetFieldFloat": [
    "float", ["string", "string"]
  ], "GetFieldBool": [
    "bool", ["string", "string"]
  ], "GetFieldByte": [
    "byte", ["string", "string"]
  ], "GetFieldShort": [
    "short", ["string", "string"]
  ], "GetFieldLong": [
    "long", ["string", "string"]
  ], "GetFieldDouble": [
    "double", ["string", "string"]
  ], "GetFieldChar": [
    "char", ["string", "string"]
  ], "GetPropertyString": [
    "string", ["string", "string"]
  ], "GetPropertyInt": [
    "int", ["string", "string"]
  ], "GetPropertyFloat": [
    "float", ["string", "string"]
  ], "GetPropertyBool": [
    "bool", ["string", "string"]
  ], "GetPropertyByte": [
    "bool", ["string", "string"]
  ], "GetPropertyShort": [
    "short", ["string", "string"]
  ], "GetPropertyLong": [
    "long", ["string", "string"]
  ], "GetPropertyDouble": [
    "double", ["string", "string"]
  ], "GetPropertyChar": [
    "char", ["string", "string"]
  ], "SetFieldString": [
    "void", ["string", "string", "string"]
  ], "SetFieldInt": [
    "void", ["string", "string", "int"]
  ], "SetFieldFloat": [
    "void", ["string", "string", "float"]
  ], "SetFieldByte": [
    "void", ["string", "string", "byte"]
  ], "SetFieldShort": [
    "void", ["string", "string", "short"]
  ], "SetFieldLong": [
    "void", ["string", "string", "long"]
  ], "SetFieldDouble": [
    "void", ["string", "string", "double"]
  ], "SetFieldChar": [
    "void", ["string", "string", "char"]
  ], "SetFieldBool": [
    "void", ["string", "string", "bool"]
  ], "SetPropertyString": [
    "void", ["string", "string", "string"]
  ], "SetPropertyInt": [
    "void", ["string", "string", "int"]
  ], "SetPropertyFloat": [
    "void", ["string", "string", "float"]
  ], "SetPropertyBool": [
    "void", ["string", "string", "bool"]
  ], "SetPropertyByte": [
    "void", ["string", "string", "byte"]
  ], "SetPropertyShort": [
    "void", ["string", "string", "short"]
  ], "SetPropertyLong": [
    "void", ["string", "string", "long"]
  ], "SetPropertyDouble": [
    "void", ["string", "string", "double"]
  ], "SetPropertyChar": [
    "void", ["string", "string", "char"]
  ], "InvokeInt": [
    "int", ["string", "string", "string"]
  ], "InvokeByte": [
    "byte", ["string", "string", "string"]
  ], "InvokeShort": [
    "short", ["string", "string", "string"]
  ], "InvokeLong": [
    "long", ["string", "string", "string"]
  ], "InvokeFloat": [
    "float", ["string", "string", "string"]
  ], "InvokeDouble": [
    "double", ["string", "string", "string"]
  ], "InvokeChar": [
    "char", ["string", "string", "string"]
  ], "InvokeString": [
    "string", ["string", "string", "string"]
  ], "InvokeBool": [
    "bool", ["string", "string", "string"]
  ]
});

// Data Type	Default Value (for fields)
// byte	        0
// short	      0
// int	        0
// long	        0L
// float	      0.0f
// double	      0.0d
// char	        '\u0000'
// string       (or any object)  	null
// bool	        false

instanceCreatorLibrary.LoadAssemblies();
// console.log(instanceCreatorLibrary.GetAssemblyTypes("TestLibrary"));
instanceCreatorLibrary.CreateInstance("TestLibrary", "TestLibrary.Box", "box1", "System.Int32 5, System.Int32 5, System.Int32 5");
instanceCreatorLibrary.SetFieldInt("box1", "Width", 5);
// instanceCreatorLibrary.SetFieldInt("box1", "Height", 5);
// instanceCreatorLibrary.SetFieldInt("box1", "Length", 5);
// console.log(instanceCreatorLibrary.GetFieldInt("box1", "Width"));

// console.log(instanceCreatorLibrary.InvokeInt("box1", "GetVolume", null));
console.log(instanceCreatorLibrary.InvokeInt("box1", "Add", "System.Int32 5, System.Int32 14"));
