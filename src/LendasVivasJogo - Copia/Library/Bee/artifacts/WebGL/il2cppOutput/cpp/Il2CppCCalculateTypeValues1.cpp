#include "pch-cpp.hpp"

#ifndef _MSC_VER
# include <alloca.h>
#else
# include <malloc.h>
#endif





struct IntPtrU5BU5D_tFD177F8C806A6921AD7150264CCC62FA00CAD832;
struct StackTraceU5BU5D_t32FBCB20930EAF5BAE3F450FF75228E5450DA0DF;
struct IDictionary_t6D03155AF1FA9083817AA5B6AD7DEEACC26AB220;
struct SafeSerializationManager_tCBB85B95DFD1634237140CD892E82D06ECB3F5E6;
struct String_t;
struct Void_t4861ACF8F4594C3437BB48B6E56783494B843915;

struct Exception_t_marshaled_com;
struct Exception_t_marshaled_pinvoke;


IL2CPP_EXTERN_C_BEGIN
IL2CPP_EXTERN_C_END

#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
struct Attribute_tFDA8EFEFB0711976D22474794576DAF28F7440AA  : public RuntimeObject
{
};
struct ConfigurationElement_tAE3EE71C256825472831FFBB7F491275DFAF089E  : public RuntimeObject
{
};
struct ConfigurationPropertyCollection_t1DEB95D3283BB11A46B862E9D13710ED698B6C93  : public RuntimeObject
{
};
struct ConfigurationSectionGroup_tE7948C2D31B193F4BA8828947ED3094B952C7863  : public RuntimeObject
{
};
struct ValueType_t6D9B272BD21782F0A9A14F2E41F85A50E97A986F  : public RuntimeObject
{
};
struct ValueType_t6D9B272BD21782F0A9A14F2E41F85A50E97A986F_marshaled_pinvoke
{
};
struct ValueType_t6D9B272BD21782F0A9A14F2E41F85A50E97A986F_marshaled_com
{
};
struct __Il2CppComDelegate_tD0DD2BBA6AC8F151D32B6DFD02F6BDA339F8DC4D  : public Il2CppComObject
{
};
struct ConfigurationCollectionAttribute_t1D7DBAAB4908B6B8F26EA1C66106A67BDE949558  : public Attribute_tFDA8EFEFB0711976D22474794576DAF28F7440AA
{
};
struct ConfigurationElementCollection_t56E8398661A85A59616301BADF13026FB1492606  : public ConfigurationElement_tAE3EE71C256825472831FFBB7F491275DFAF089E
{
};
struct ConfigurationSection_t0BC609F0151B160A4FAB8226679B62AF22539C3E  : public ConfigurationElement_tAE3EE71C256825472831FFBB7F491275DFAF089E
{
};
struct Int32_t680FF22E76F6EFAD4375103CBBFFA0421349384C 
{
	int32_t ___m_value;
};
struct IntPtr_t 
{
	void* ___m_value;
};
struct Exception_t  : public RuntimeObject
{
	String_t* ____className;
	String_t* ____message;
	RuntimeObject* ____data;
	Exception_t* ____innerException;
	String_t* ____helpURL;
	RuntimeObject* ____stackTrace;
	String_t* ____stackTraceString;
	String_t* ____remoteStackTraceString;
	int32_t ____remoteStackIndex;
	RuntimeObject* ____dynamicMethods;
	int32_t ____HResult;
	String_t* ____source;
	SafeSerializationManager_tCBB85B95DFD1634237140CD892E82D06ECB3F5E6* ____safeSerializationManager;
	StackTraceU5BU5D_t32FBCB20930EAF5BAE3F450FF75228E5450DA0DF* ___captured_traces;
	IntPtrU5BU5D_tFD177F8C806A6921AD7150264CCC62FA00CAD832* ___native_trace_ips;
	int32_t ___caught_in_unmanaged;
};
struct Exception_t_marshaled_pinvoke
{
	char* ____className;
	char* ____message;
	RuntimeObject* ____data;
	Exception_t_marshaled_pinvoke* ____innerException;
	char* ____helpURL;
	Il2CppIUnknown* ____stackTrace;
	char* ____stackTraceString;
	char* ____remoteStackTraceString;
	int32_t ____remoteStackIndex;
	Il2CppIUnknown* ____dynamicMethods;
	int32_t ____HResult;
	char* ____source;
	SafeSerializationManager_tCBB85B95DFD1634237140CD892E82D06ECB3F5E6* ____safeSerializationManager;
	StackTraceU5BU5D_t32FBCB20930EAF5BAE3F450FF75228E5450DA0DF* ___captured_traces;
	Il2CppSafeArray* ___native_trace_ips;
	int32_t ___caught_in_unmanaged;
};
struct Exception_t_marshaled_com
{
	Il2CppChar* ____className;
	Il2CppChar* ____message;
	RuntimeObject* ____data;
	Exception_t_marshaled_com* ____innerException;
	Il2CppChar* ____helpURL;
	Il2CppIUnknown* ____stackTrace;
	Il2CppChar* ____stackTraceString;
	Il2CppChar* ____remoteStackTraceString;
	int32_t ____remoteStackIndex;
	Il2CppIUnknown* ____dynamicMethods;
	int32_t ____HResult;
	Il2CppChar* ____source;
	SafeSerializationManager_tCBB85B95DFD1634237140CD892E82D06ECB3F5E6* ____safeSerializationManager;
	StackTraceU5BU5D_t32FBCB20930EAF5BAE3F450FF75228E5450DA0DF* ___captured_traces;
	Il2CppSafeArray* ___native_trace_ips;
	int32_t ___caught_in_unmanaged;
};
struct IgnoreSection_t43A7C33C0083D18639AA3CC3D75DD93FCF1C5D97  : public ConfigurationSection_t0BC609F0151B160A4FAB8226679B62AF22539C3E
{
};
struct SystemException_tCC48D868298F4C0705279823E34B00F4FBDB7295  : public Exception_t
{
};
struct InvalidOperationException_t5DDE4D49B7405FAAB1E4576F4715A42A3FAD4BAB  : public SystemException_tCC48D868298F4C0705279823E34B00F4FBDB7295
{
};
struct ObjectDisposedException_tC5FB29E8E980E2010A2F6A5B9B791089419F89EB  : public InvalidOperationException_t5DDE4D49B7405FAAB1E4576F4715A42A3FAD4BAB
{
	String_t* ____objectName;
};
struct ThrowStub_t9161280E38728A40D9B1A975AEE62E89C379E400  : public ObjectDisposedException_tC5FB29E8E980E2010A2F6A5B9B791089419F89EB
{
};
#ifdef __clang__
#pragma clang diagnostic pop
#endif



#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
extern const Il2CppTypeDefinitionSizes g_typeDefinitionSize5400;
const Il2CppTypeDefinitionSizes g_typeDefinitionSize5400 = { sizeof(ConfigurationElement_tAE3EE71C256825472831FFBB7F491275DFAF089E), -1, 0, 0 };
extern const Il2CppTypeDefinitionSizes g_typeDefinitionSize5401;
const Il2CppTypeDefinitionSizes g_typeDefinitionSize5401 = { sizeof(ConfigurationSection_t0BC609F0151B160A4FAB8226679B62AF22539C3E), -1, 0, 0 };
extern const Il2CppTypeDefinitionSizes g_typeDefinitionSize5402;
const Il2CppTypeDefinitionSizes g_typeDefinitionSize5402 = { sizeof(int32_t)+ sizeof(RuntimeObject), sizeof(int32_t), 0, 0 };
extern const Il2CppTypeDefinitionSizes g_typeDefinitionSize5403;
const Il2CppTypeDefinitionSizes g_typeDefinitionSize5403 = { sizeof(ConfigurationPropertyCollection_t1DEB95D3283BB11A46B862E9D13710ED698B6C93), -1, 0, 0 };
extern const Il2CppTypeDefinitionSizes g_typeDefinitionSize5404;
const Il2CppTypeDefinitionSizes g_typeDefinitionSize5404 = { sizeof(ConfigurationElementCollection_t56E8398661A85A59616301BADF13026FB1492606), -1, 0, 0 };
extern const Il2CppTypeDefinitionSizes g_typeDefinitionSize5405;
const Il2CppTypeDefinitionSizes g_typeDefinitionSize5405 = { sizeof(ConfigurationCollectionAttribute_t1D7DBAAB4908B6B8F26EA1C66106A67BDE949558), -1, 0, 0 };
extern const Il2CppTypeDefinitionSizes g_typeDefinitionSize5406;
const Il2CppTypeDefinitionSizes g_typeDefinitionSize5406 = { sizeof(ConfigurationSectionGroup_tE7948C2D31B193F4BA8828947ED3094B952C7863), -1, 0, 0 };
extern const Il2CppTypeDefinitionSizes g_typeDefinitionSize5407;
const Il2CppTypeDefinitionSizes g_typeDefinitionSize5407 = { sizeof(IgnoreSection_t43A7C33C0083D18639AA3CC3D75DD93FCF1C5D97), -1, 0, 0 };
extern const Il2CppTypeDefinitionSizes g_typeDefinitionSize5408;
const Il2CppTypeDefinitionSizes g_typeDefinitionSize5408 = { sizeof(ThrowStub_t9161280E38728A40D9B1A975AEE62E89C379E400), -1, 0, 0 };
extern const Il2CppTypeDefinitionSizes g_typeDefinitionSize5409;
const Il2CppTypeDefinitionSizes g_typeDefinitionSize5409 = { 0, sizeof(Il2CppIActivationFactory*), 0, 0 };
extern const Il2CppTypeDefinitionSizes g_typeDefinitionSize5410;
const Il2CppTypeDefinitionSizes g_typeDefinitionSize5410 = { sizeof(Il2CppComObject), -1, 0, 0 };
extern const Il2CppTypeDefinitionSizes g_typeDefinitionSize5411;
const Il2CppTypeDefinitionSizes g_typeDefinitionSize5411 = { sizeof(__Il2CppComDelegate_tD0DD2BBA6AC8F151D32B6DFD02F6BDA339F8DC4D), -1, 0, 0 };
extern const Il2CppTypeDefinitionSizes g_typeDefinitionSize5412;
const Il2CppTypeDefinitionSizes g_typeDefinitionSize5412 = { sizeof(Il2CppFullySharedGenericAny), -1, 0, 0 };
extern const Il2CppTypeDefinitionSizes g_typeDefinitionSize5413;
const Il2CppTypeDefinitionSizes g_typeDefinitionSize5413 = { sizeof(Il2CppFullySharedGenericStruct)+ sizeof(RuntimeObject), -1, 0, 0 };
#ifdef __clang__
#pragma clang diagnostic pop
#endif
