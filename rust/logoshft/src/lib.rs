use std::ffi::{c_char, CStr};
use tokenizers::tokenizer::Tokenizer;

pub struct TokenizerHandle {
    tokenizer: Tokenizer,
}

#[no_mangle]
pub extern "C" fn create_tokenizer(tokenizer_json: *const c_char) -> *mut TokenizerHandle {
    let tokenizer_slice = unsafe {
        match CStr::from_ptr(tokenizer_json).to_bytes() {
            bytes => match Tokenizer::from_bytes(bytes) {
                Ok(tokenizer) => Box::into_raw(Box::new(TokenizerHandle { tokenizer })),
                Err(_) => std::ptr::null_mut(),
            }
        }
    };
    tokenizer_slice
}

#[no_mangle]
pub extern "C" fn destroy_tokenizer(handle: *mut TokenizerHandle) {
    if !handle.is_null() {
        unsafe {
            drop(Box::from_raw(handle));
        }
    }
}

#[no_mangle]
pub extern "C" fn count_tokens(handle: *const TokenizerHandle, text: *const c_char) -> u32 {
    if handle.is_null() {
        return 0;
    }

    let text_slice = unsafe {
        match CStr::from_ptr(text).to_str() {
            Ok(s) => s,
            Err(_) => return 0,
        }
    };

    unsafe {
        (*handle).tokenizer.encode(text_slice, false)
            .map(|encoding| encoding.get_tokens().len() as u32)
            .unwrap_or(0)
    }
}

#[cfg(test)]
mod tests {
    use super::*;
    use std::ffi::CString;
    use std::fs;

    #[test]
    fn test_count_tokens() {
        let tokenizer_json = fs::read("../../tests/tokenizer.json").unwrap();
        let tokenizer_cstr = CString::new(tokenizer_json).unwrap();
        let handle = create_tokenizer(tokenizer_cstr.as_ptr());
        assert!(!handle.is_null());

        let test_str = CString::new("Hello, world!").unwrap();
        let result = count_tokens(handle, test_str.as_ptr());
        assert_eq!(result, 4);

        destroy_tokenizer(handle as *mut _);
    }
}
