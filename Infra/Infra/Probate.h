#pragma once

#include "Prudate.h"

Int String_QStringSet(Int result, Int a);
Int String_QStringSetRaw(Int result, Int a);

Int Stream_FlushStorage(Int device);
Int Stream_FlushNetwork(Int device);
Int Stream_ValueGet(Int o);
Int Stream_ValueSet(Int o, Int value);
Int Stream_CanReadSet(Int o, Int value);
Int Stream_CanWriteSet(Int o, Int value);
Int Stream_Intern(Int o);






Int Network_ConnectedOpen(Int o);


Int Network_GetOpenSocket(Int o);



Int Network_ServerSet(Int o, Int socket);



Int Network_CaseChanged(Int o);


Int Network_Error(Int o);


Int Network_ReadyRead(Int o);



Int NetworkServer_NewPeer(Int o);




Int NetworkAddress_Intern(Int o);




Int Frame_GetMouseEvent(Int o);




Int Draw_Intern(Int o);

Int Brush_Intern(Int o);




Int Pen_Intern(Int o);




Int Image_Intern(Int o);




Int Image_SetReadIntern(Int o, Int value);




Int Font_Intern(Int o);




Int Transform_Intern(Int o);

Int Gradient_Intern(Int o);



Int GradientLinear_Intern(Int o);
Int GradientRadial_Intern(Int o);

Int VideoOut_Intern(Int o);
Int VideoOut_FrameChange(Int o);

Int VideoFrame_Intern(Int o);



Int AudioOut_Intern(Int o);




Int GetInternValue(Int a);



Int GetValueFromInternValue(Int a);




Int Math_GetInternValue(Int o, Int a);



Int Math_GetValueFromInternValue(Int o, Int a);





Int Thread_HandleGet(Int o);
Int Thread_HandleSet(Int o, Int value);





Int Thread_GetInternCaseMutex(Int o);



Int Thread_GetInternHandleSemaphore(Int o);




Bool Thread_InitMainThread(Int o);



Bool Thread_FinalMainThread(Int o);





Int Thread_OS_OpenHandle(Int threadId);



Int Thread_OS_CloseHandle(Int handle);



Int Thread_OS_Set();



Int Thread_OS_Pause(Int handle);



Int Thread_OS_Resume(Int handle);





Int Thread_CreateStore();



Int Thread_DeleteStore(Int a);




Int Thread_StoreSetThread(Int thread);





Int Interval_Elapse(Int o);




Int Post_ExecuteHandle(Int o);




Int Process_Started(Int o);



Int Process_Finished(Int o);




Bool Main_SetCurrentThreadSignalHandle();




Int Console_OS_Init();





Int HasFlag(Int value, Int flag);






Int Share_New();



Bool Share_Delete(Int o);



Bool Share_Init(Int o);



Bool Share_Final(Int o);





Int Share_ThreadStorage(Int o);





Int Stat_New();



Bool Stat_Delete(Int o);



Bool Stat_Init(Int o);



Bool Stat_Final(Int o);




Int Stat_TimeInit(Int o);



#define FieldGet(varClass, name) \
Int varClass##_##name##Get(Int o)\
{\
    varClass* m;\
    m = CastPointer(o);\
    return m->name;\
}\


#define FieldSet(varClass, name) \
Int varClass##_##name##Set(Int o, Int value)\
{\
    varClass* m;\
    m = CastPointer(o);\
    m->name = value;\
    return true;\
}\


#define Field(varClass, name) \
FieldGet(varClass, name)\
FieldSet(varClass, name)
