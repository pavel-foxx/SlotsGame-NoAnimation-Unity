//
//  OrientationNavigationViewController.m
//  Unity-iPhone
//
//  Created by Piotr on 16/07/17.
//
//

#import "OrientationNavigationViewController.h"

@interface OrientationNavigationViewController ()

@end

@implementation OrientationNavigationViewController

- (UIInterfaceOrientationMask)supportedInterfaceOrientations {
    if (_shouldStickToPortrait) {
        return UIInterfaceOrientationMaskPortrait;
    } else if (_shouldStickToLandscape) {
        return UIInterfaceOrientationMaskLandscape;
    }
    
    return UIInterfaceOrientationMaskAll;
}


@end
