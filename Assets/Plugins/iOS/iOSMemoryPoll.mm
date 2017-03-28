#include <mach/mach.h> // this include you need to be able to use the task_info

extern "C"
{
int report_memory(void)
{
    struct task_basic_info info;
    mach_msg_type_number_t size = sizeof(info); kern_return_t kerr = task_info(mach_task_self(), TASK_BASIC_INFO, (task_info_t)&info, &size);
    
    return info.resident_size/1024/1024; // this is the value that you need to use
}
}