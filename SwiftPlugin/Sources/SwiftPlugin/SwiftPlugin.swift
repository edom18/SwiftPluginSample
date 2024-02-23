import Foundation

@_cdecl("calc")
public func calc(a: Int32, b: Int32) -> Int32 {
    return a + b
}

public class NativeUtility {
    
    public init() {
        
    }
    
    public func add(a: Int32, b: Int32) -> Int32 {
        a + b
    }
    
    public func sub(a: Int32, b: Int32) -> Int32 {
        a - b
    }
    
    public func version() -> String {
        "1.0.0"
    }
    
    public func stringDecoration(str: String) -> String {
        "Decorated[\(str)]"
    }
}

@_cdecl("create_instance")
public func create_instance() -> UnsafeMutableRawPointer {
    let utility: NativeUtility = NativeUtility()
    return Unmanaged.passRetained(utility).toOpaque()
}

@_cdecl("use_utility_add")
public func use_utility_add(_ pointer: UnsafeMutableRawPointer, _ a: Int32, _ b: Int32) -> Int32 {
    let instance: NativeUtility = Unmanaged<NativeUtility>.fromOpaque(pointer).takeUnretainedValue()
    return instance.add(a: a, b: b)
}

@_cdecl("use_utility_sub")
public func use_utility_sub(_ pointer: UnsafeMutableRawPointer, _ a: Int32, _ b: Int32) -> Int32 {
    let instance: NativeUtility = Unmanaged<NativeUtility>.fromOpaque(pointer).takeUnretainedValue()
    return instance.sub(a: a, b: b)
}

@_cdecl("use_utility_version")
public func use_utility_version(_ pointer: UnsafeMutableRawPointer) -> UnsafeMutablePointer<CChar> {
    let instance: NativeUtility = Unmanaged<NativeUtility>.fromOpaque(pointer).takeUnretainedValue()
    let version = instance.version()
    return strdup(version)
}

@_cdecl("use_utility_decorate")
public func use_utility_decorate(_ pointer: UnsafeMutableRawPointer, _ text: UnsafePointer<CChar>) -> UnsafeMutablePointer<CChar> {
    let instance: NativeUtility = Unmanaged<NativeUtility>.fromOpaque(pointer).takeUnretainedValue()
    
    let string = String(cString: text)
    let result = instance.stringDecoration(str: string)
    return strdup(result)
}
