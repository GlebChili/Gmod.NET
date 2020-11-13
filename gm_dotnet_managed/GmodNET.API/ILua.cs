﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Numerics;
using System.Runtime.InteropServices;

namespace GmodNET.API
{
    /// <summary>
    /// Managed wrapper around Garry's Mod native ILuaBase.
    /// </summary>
    public interface ILua
    {
        /// <summary>
        /// Returns the amount of values on the stack.
        /// </summary>
        /// <returns></returns>
        public int Top();
        /// <summary>
        /// Pushes a copy of the value at iStackPos to the top of the stack.
        /// </summary>
        /// <param name="iStackPos">Position of the value on the stack</param>
        public void Push(int iStackPos);
        /// <summary>
        /// Pops iAmt values from the top of the stack.
        /// </summary>
        /// <param name="IAmt">Amount of values to pop</param>
        public void Pop(int IAmt = 1);
        /// <summary>
        /// Pushes table[key] on to the stack.
        /// </summary>
        /// <param name="iStackPos">Position of the table on the stack</param>
        /// <param name="key">Key in the table</param>
        public void GetField(int iStackPos, in string key);
        /// <summary>
        /// Sets table[key] to the value at the top of the stack. Pops value from the stack.
        /// </summary>
        /// <param name="iStackPos">Position of the table on the stack</param>
        /// <param name="key">Key in the table</param>
        public void SetField(int iStackPos, in string key);
        /// <summary>
        /// Creates a new table and pushes it to the top of the stack.
        /// </summary>
        public void CreateTable();
        /// <summary>
        /// Sets the metatable for the value at iStackPos to the value at the top of the stack. Pops the value off of the top of the stack.
        /// </summary>
        /// <param name="iStackPos">Position of object ot set metatable to</param>
        public void SetMetaTable(int iStackPos);
        /// <summary>
        /// Pushes the metatable of the value at iStackPos on to the top of the stack. Upon failure, returns false and does not push anything.
        /// </summary>
        /// <param name="iStackPos">Position of the object to get metatable from</param>
        /// <returns>Success indicator</returns>
        public bool GetMetaTable(int iStackPos);
        /// <summary>
        /// Calls a function. To use it: Push the function on to the stack followed by each argument. 
        /// Pops the function and arguments from the stack, leaves iResults values on the stack.
        /// This method is obsolete and unsafe. Use ILua.MCall or ILua.PCall instead.
        /// </summary>
        /// <param name="iArgs">Number of arguments of the function</param>
        /// <param name="iResults">Number of return values of the function</param>
        [Obsolete("This method is obsolete, unsafe and may be removed in a future. Use ILua.MCall or ILua.PCall instead.", false)]
        public void Call(int iArgs, int iResults);
        /// <summary>
        /// Similar to Call. Calls a function in protected mode. Both nargs and nresults have the same meaning as in lua_call.
        /// If there are no errors during the call, lua_pcall behaves exactly like lua_call.
        /// However, if there is any error, lua_pcall catches it, pushes a single value on the stack (the error message), and returns an error code.
        /// Like lua_call, lua_pcall always removes the function and its arguments from the stack.
        /// If errfunc is 0, then the error message returned on the stack is exactly the original error message.
        /// Otherwise, errfunc is the stack index of an error handler function.
        /// (In the current implementation, this index cannot be a pseudo-index.)
        /// In case of runtime errors, this function will be called with the error message and its return value will be the message returned on the stack by lua_pcall.
        /// </summary>
        /// <param name="IArgs">Number of arguments of the function</param>
        /// <param name="IResults">Number of return values of the function</param>
        /// <param name="ErrorFunc">The stack index of an error handler function</param>
        /// <returns>0 in case of success or one of the error codes (defined by lua engine)</returns>
        public int PCall(int IArgs, int IResults, int ErrorFunc);
        /// <summary>
        /// Returns true if the values at iA and iB are equal.
        /// </summary>
        /// <param name="iA">Position of the first value to compare</param>
        /// <param name="iB">Position of the second value</param>
        /// <returns></returns>
        public bool Equal(int iA, int iB);
        /// <summary>
        /// Returns true if the value at iA and iB are equal. Does not invoke metamethods.
        /// </summary>
        /// <param name="iA">Position of the first value to compare</param>
        /// <param name="iB">Position of the second value</param>
        /// <returns></returns>
        public bool RawEqual(int iA, int iB);
        /// <summary>
        /// Moves the value at the top of the stack in to iStackPos. Any elements above iStackPos are shifted upwards.
        /// </summary>
        /// <param name="iStackPos">Position on the stack</param>
        public void Insert(int iStackPos);
        /// <summary>
        /// Removes the value at iStackPos from the stack. Any elements above iStackPos are shifted downwards.
        /// </summary>
        /// <param name="iStackPos">Position on the stack</param>
        public void Remove(int iStackPos);
        /// <summary>
        /// Allows you to iterate tables similar to pairs(...). 
        /// Pops a key from the stack, and pushes a key-value pair from the table at the given index (the "next" pair after the given key).
        /// If there are no more elements in the table, then lua_next returns 0 (and pushes nothing).
        /// </summary>
        /// <param name="iStackPos">Position of the table</param>
        /// <returns></returns>
        public int Next(int iStackPos);
        /// <summary>
        /// Returns the string at iStackPos. iOutLen is set to the length of the string if it is not NULL. 
        /// If the value at iStackPos is a number, it will be converted in to a string.
        /// Returns empty string upon failure.
        /// </summary>
        /// <param name="iStackPos"></param>
        /// <returns></returns>
        public string GetString(int iStackPos);
        /// <summary>
        /// Returns the number at iStackPos. Returns 0 upon failure.
        /// </summary>
        /// <param name="iStackPos">Position of number of the stack</param>
        /// <returns></returns>
        public double GetNumber(int iStackPos);
        /// <summary>
        /// Returns the boolean at iStackPos (as int). Returns false upon failure.
        /// </summary>
        /// <param name="iStackPos">Position on the stack</param>
        /// <returns></returns>
        public bool GetBool(int iStackPos);
        /// <summary>
        /// Returns the C-Function at iStackPos (native pointer). Returns NULL upon failure.
        /// </summary>
        /// <param name="iStackPos">Position on the stack</param>
        /// <returns></returns>
        public IntPtr GetCFunction(int iStackPos);
        /// <summary>
        /// Pushes a nil value on to the stack
        /// </summary>
        public void PushNil();
        /// <summary>
        /// Pushes the given string on to the stack.
        /// </summary>
        /// <param name="str">String to push</param>
        public void PushString(in string str);
        /// <summary>
        /// Pushes the given double on to the stack.
        /// </summary>
        /// <param name="val">Number to push</param>
        public void PushNumber(double val);
        /// <summary>
        /// Pushes the given boolean on to the stack.
        /// </summary>
        /// <param name="val">Bool value to push</param>
        public void PushBool(bool val);
        /// <summary>
        /// Pushes the given C-Function on to the stack. Native C function must be of signature "int Func(void*)".
        /// </summary>
        /// <param name="native_func_ptr">Native C function pointer to push</param>
        public unsafe void PushCFunction(IntPtr native_func_ptr);
        /// <summary>
        /// Pushes a given C# function pointer with native Cdecl calling convention onto the Lua stack.
        /// </summary>
        /// <remarks>
        /// Function which pointer is pushed onto the Lua stack must conform lua_CFunction specification as described here: https://www.lua.org/pil/26.1.html
        /// </remarks>
        /// <param name="function_pointer">C# function pointer with native calling convention Cdecl.</param>
        public unsafe void PushCFunction(delegate* unmanaged[Cdecl]<IntPtr, int> function_pointer);
        /// <summary>
        /// Pushes the given C-Function on to the stack with upvalues.
        /// </summary>
        /// <param name="native_func_ptr"></param>
        /// <param name="iVars"></param>
        public void PushCClosure(IntPtr native_func_ptr, int iVars);
        /// <summary>
        /// Allows for values to be stored by reference for later use. Make sure you call ReferenceFree when you are done with a reference. 
        /// Pops the value to reference from the stack.
        /// </summary>
        /// <returns></returns>
        public int ReferenceCreate();
        /// <summary>
        /// Free reference
        /// </summary>
        /// <param name="reference">Reference to free</param>
        public void ReferenceFree(int reference);
        /// <summary>
        /// Push reference on to the stack
        /// </summary>
        /// <param name="reference">Reference to push</param>
        public void ReferencePush(int reference);
        /// <summary>
        /// Push a special value onto the top of the stack.
        /// </summary>
        /// <param name="table">Table to push</param>
        public void PushSpecial(SPECIAL_TABLES table);
        /// <summary>
        /// Returns true if the value at iStackPos is of type iType.
        /// </summary>
        /// <param name="iStackPos">Position of value to check type of</param>
        /// <param name="iType">Type index</param>
        /// <returns></returns>
        public bool IsType(int iStackPos, int iType);
        /// <summary>
        /// Returns true if the value at iStackPos is of the given type.
        /// </summary>
        /// <param name="iStackPos">Position of value to check type of</param>
        /// <param name="type">Type</param>
        /// <returns></returns>
        public bool IsType(int iStackPos, TYPES type);
        /// <summary>
        /// Returns the type of the value at iStackPos.
        /// </summary>
        /// <param name="iStackPos"></param>
        /// <returns></returns>
        public int GetType(int iStackPos);
        /// <summary>
        /// Returns the name associated with the given type ID. Doesn't work with user-defined types.
        /// </summary>
        /// <param name="iType">Type index</param>
        /// <returns></returns>
        public string GetTypeName(int iType);
        /// <summary>
        /// Returns the name associated with the given type.
        /// </summary>
        /// <param name="type">Type</param>
        /// <returns></returns>
        public string GetTypeName(TYPES type);
        /// <summary>
        /// Returns the length of the object at iStackPos.
        /// </summary>
        /// <param name="iStackPos">Position on the stack</param>
        /// <returns></returns>
        public int ObjLen(int iStackPos);
        /// <summary>
        /// Returns the angle at iStackPos as C# Vector3.
        /// </summary>
        /// <param name="iStackPos">Position on the stack</param>
        /// <returns></returns>
        public Vector3 GetAngle(int iStackPos);
        /// <summary>
        /// Returns the vector at iStackPos.
        /// </summary>
        /// <param name="iStackPos">Position on the stack</param>
        /// <returns></returns>
        public Vector3 GetVector(int iStackPos);
        /// <summary>
        /// Pushes the given angle to the top of the stack.
        /// </summary>
        /// <param name="ang">Angle (Vector3 represented) to push</param>
        public void PushAngle(Vector3 ang);
        /// <summary>
        /// Pushes the given vector to the top of the stack.
        /// </summary>
        /// <param name="vec">Vector to push</param>
        public void PushVector(Vector3 vec);
        /// <summary>
        /// Sets the lua_State to be used by the ILuaBase implementation.
        /// </summary>
        /// <param name="lua_state">Pointer to the lua_state</param>
        public void SetState(IntPtr lua_state);
        /// <summary>
        /// Pushes the metatable associated with the given type name.
        /// Returns the type ID to use for this type.
        /// </summary>
        /// <param name="name">Name of the metatable</param>
        /// <returns>ID (type index) of the metatable</returns>
        public int CreateMetaTable(in string name);
        /// <summary>
        /// Pushes the metatable associated with the given type.
        /// </summary>
        /// <param name="iType">Index of the type which metatable to push</param>
        /// <returns>Success indicator</returns>
        public bool PushMetaTable(int iType);
        /// <summary>
        /// Pushes the metatable associated with the given type.
        /// </summary>
        /// <param name="type">Type which metatable to push</param>
        /// <returns>Success indicator</returns>
        public bool PushMetaTable(TYPES type);
        /// <summary>
        /// Creates a new UserData of type iType that references the given data.
        /// </summary>
        /// <param name="data_pointer">Pointer to data to reference as user data</param>
        /// <param name="iType">Type index</param>
        public void PushUserType(IntPtr data_pointer, int iType);
        /// <summary>
        /// Sets the data pointer of the UserType at iStackPos. You can use this to invalidate a UserType by passing NULL.
        /// </summary>
        /// <param name="iStackPos">Position of object on the stack</param>
        /// <param name="data_pointer">User data pointer</param>
        public void SetUserType(int iStackPos, IntPtr data_pointer);
        /// <summary>
        /// Returns the data of the UserType at iStackPos if it is of the given type.
        /// </summary>
        /// <param name="iStackPos">Position on the stack</param>
        /// <param name="iType">Type index</param>
        /// <returns>pointer to the user type</returns>
        public IntPtr GetUserType(int iStackPos, int iType);
        /// <summary>
        /// Pushes table[key] on to the stack. Table = value at iStackPos. Key = value at top of the stack.
        /// Pops the key from the stack
        /// </summary>
        /// <param name="iStackPos">Position of the table on the stack</param>
        public void GetTable(int iStackPos);
        /// <summary>
        /// Sets table[key] to the value at the top of the stack. Table = value at iStackPos. Key = value 2nd to the top of the stack.
        /// Pops the key and the value from the stack.
        /// </summary>
        /// <param name="iStackPos">Position of the table on the stack</param>
        public void SetTable(int iStackPos);
        /// <summary>
        /// Pushes table[key] on to the stack. Table = value at iStackPos. Key = value at top of the stack. Does not invoke metamethods.
        /// </summary>
        /// <param name="iStackPos">Position of the table on the stack</param>
        public void RawGet(int iStackPos);
        /// <summary>
        /// Sets table[key] to the value at the top of the stack. Table = value at iStackPos. Key = value 2nd to the top of the stack.
        /// Pops the key and the value from the stack. Does not invoke metamethods.
        /// </summary>
        /// <param name="iStackPos">Position of the table on the stack</param>
        public void RawSet(int iStackPos);
        /// <summary>
        /// Pushes the given pointer on to the stack as light-userdata.
        /// </summary>
        /// <param name="data">Pointer to the user data</param>
        [Obsolete("This method is unsafe and obsolete. Use ILua.PushUserType instead")]
        public void PushUserData(IntPtr data);
        /// <summary>
        /// Get ILuaBase native pointer from Garry's Mod.
        /// </summary>
        /// <returns></returns>
        public IntPtr GetInternalPointer();
        /// <summary>
        /// High level wrapper around PCall. If call is successfull, MCall will behave just like Call. 
        /// But if Lua exception is thrown while call, GmodLuaException managed exception will be thrown.
        /// </summary>
        /// <param name="iArgs">Number of arguments of the function to call</param>
        /// <param name="iResults">Number of returns of the function to call</param>
        public void MCall(int iArgs, int iResults);
        /// <summary>
        /// Push a managed function or delegate to the lua stack.
        /// </summary>
        /// <param name="function">A managed function or delegate to push.</param>
        public void PushManagedFunction(Func<ILua, int> function);
        /// <summary>
        /// Push managed function or delegate together with upvalues as Lua closure. Upvalues must be pushed first. Pops upvalues from the stack.
        /// </summary>
        /// <param name="function">Managed function or delegate to form closure from.</param>
        /// <param name="number_of_upvalues">Number of upvalues.</param>
        public void PushManagedClosure(Func<ILua, int> function, byte number_of_upvalues);
    }

    /// <summary>
    /// Managed exception which incapsulates information about Lua exception
    /// </summary>
    public class GmodLuaException : Exception
    {
        int error_code;

        /// <summary>
        /// Create new GmodLuaException
        /// </summary>
        /// <param name="lua_error_code">Lua exception code</param>
        /// <param name="lua_error_message">Lua exception message</param>
        public GmodLuaException(int lua_error_code, string lua_error_message) : base(lua_error_message)
        {
            this.error_code = lua_error_code;
        }

        /// <summary>
        /// Error code of the lua exception
        /// </summary>
        public int ErrorCode => error_code;
        /// <summary>
        /// Lua exception message
        /// </summary>
        public override string Message => base.Message;
    }

    /// <summary>
    /// Indeces of the Lua special tables.
    /// </summary>
    public enum SPECIAL_TABLES
    {
        /// <summary>
        /// Global table.
        /// </summary>
        SPECIAL_GLOB,
        /// <summary>
        /// Environment table.
        /// </summary>
        SPECIAL_ENV,
        /// <summary>
        /// Unknown. TODO.
        /// </summary>
        SPECIAL_REG
    }

    /// <summary>
    /// The indices of common Lua and Garry's Mod built-in types.
    /// </summary>
    public enum TYPES
    {
        NONE = -1,
        NIL,
        BOOL,
        LIGHTUSERDATA,
        NUMBER,
        STRING,
        TABLE,
        FUNCTION,
        USERDATA,
        THREAD,

        // GMod Types
        ENTITY,
        Vector, // GMOD: GO TODO - This was renamed... I'll probably forget to fix it before this ends up public
        ANGLE,
        PHYSOBJ,
        SAVE,
        RESTORE,
        DAMAGEINFO,
        EFFECTDATA,
        MOVEDATA,
        RECIPIENTFILTER,
        USERCMD,
        SCRIPTEDVEHICLE,
        MATERIAL,
        PANEL,
        PARTICLE,
        PARTICLEEMITTER,
        TEXTURE,
        USERMSG,
        CONVAR,
        IMESH,
        MATRIX,
        SOUND,
        PIXELVISHANDLE,
        DLIGHT,
        VIDEO,
        FILE,
        LOCOMOTION,
        PATH,
        NAVAREA,
        SOUNDHANDLE,
        NAVLADDER,
        PARTICLESYSTEM,
        PROJECTEDTEXTURE,
        PHYSCOLLIDE,
        SURFACEINFO,

        COUNT
    }
}
