import SwiftPlugin

@_cdecl("add")
public func add(_ a: Int32, _ b: Int32) -> Int32 {
    let utility: NativeUtility = NativeUtility()
    
    let result: Int32 = utility.add(a: a, b: b)
    
    return result
}
