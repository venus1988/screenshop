//
//  IOSNative.m
//
//  Created by Osipov Stanislav on 1/11/13.
//
//

#import "IOSTwitterPlugin.h"



@implementation IOSTwitterPlugin


static IOSTwitterPlugin *_sharedInstance;


+ (id)sharedInstance {
    
    if (_sharedInstance == nil)  {
        _sharedInstance = [[self alloc] init];
    }
    
    return _sharedInstance;
}


- (void) initTwitterPlugin {
    NSLog(@"MSP: Twitter init");
    
    NSString * status = @"0";
    
    if([self IsTwitterAvaliable]) {
        if([self IsTwitterAuthed]) {
            status = @"1";
        }
    }
    
    NSLog(@"MSP: Status init %@", status);
    UnitySendMessage("IOSTwitterManager", "OnInited", [ISNDataConvertor NSStringToChar:status]);
    
}

-(void) authificateUser {
    NSLog(@"MSP:  authificateUser");
    ACAccountStore *account = [[ACAccountStore alloc] init];
    ACAccountType *twitterAccountType = [account accountTypeWithAccountTypeIdentifier:ACAccountTypeIdentifierTwitter];
    
    [account requestAccessToAccountsWithType:twitterAccountType options:NULL completion:^(BOOL granted, NSError *error)  {
        if (granted) {
            NSArray *twitterAccounts = [account accountsWithAccountType:twitterAccountType];
            if ([twitterAccounts count] > 0) {
                
                NSLog(@"MSP:  OnAuthSuccess");
                UnitySendMessage("IOSTwitterManager", "OnAuthSuccess", [ISNDataConvertor NSStringToChar:@""]);
                
            } else {
                NSLog(@"MSP: OnAuthFailed no aacounts");
                UnitySendMessage("IOSTwitterManager", "OnAuthFailed", [ISNDataConvertor NSStringToChar:@"0"]);
            }
            
        } else {
            NSLog(@"MSP: OnAuthFailed no accses");
            UnitySendMessage("IOSTwitterManager", "OnAuthFailed", [ISNDataConvertor NSStringToChar:@"1"]);
        }
    }];

    
}


-(void) loadUserData {
    
    NSLog(@"MSP: loadUserData");
    ACAccountStore *account = [[ACAccountStore alloc] init];
    ACAccountType *twitterAccountType = [account accountTypeWithAccountTypeIdentifier:ACAccountTypeIdentifierTwitter];
    
    [account requestAccessToAccountsWithType:twitterAccountType options:NULL completion:^(BOOL granted, NSError *error)  {
        if (granted) {
            NSArray *twitterAccounts = [account accountsWithAccountType:twitterAccountType];
            if ([twitterAccounts count] > 0) {
                ACAccount *twitterAccount = [twitterAccounts objectAtIndex:0];
               
                // Creating a request to get the info about a user on Twitter
                NSLog(@"MSP: Using twitter acc with name: %@", twitterAccount.username);
                
                SLRequest *twitterInfoRequest = [SLRequest requestForServiceType:SLServiceTypeTwitter requestMethod:SLRequestMethodGET URL:[NSURL URLWithString:@"https://api.twitter.com/1.1/users/show.json"] parameters:[NSDictionary dictionaryWithObject:twitterAccount.username forKey:@"screen_name"]];
                [twitterInfoRequest setAccount:twitterAccount];
                
                
                
                
                // Making the request
                [twitterInfoRequest performRequestWithHandler:^(NSData *responseData, NSHTTPURLResponse *urlResponse, NSError *error) {
                    dispatch_async(dispatch_get_main_queue(), ^{
                        NSLog(@"MSP: twitterInfoRequest finished");

                        
                        // Check if we reached the reate limit
                        if ([urlResponse statusCode] == 429) {
                            NSLog(@"MSP: Rate limit reached");
                            UnitySendMessage("IOSTwitterManager", "OnUserDataLoadFailed", [ISNDataConvertor NSStringToChar:@""]);
                            return;
                        }
                        
                        // Check if there was an error
                        if (error) {
                            NSLog(@"MSP: Error: %@", error.localizedDescription);
                            UnitySendMessage("IOSTwitterManager", "OnUserDataLoadFailed", [ISNDataConvertor NSStringToChar:@""]);
                            return;
                        }
                        
                        // Check if there is some response data
                        if (responseData) {
                           NSString *resp =  [[NSString alloc] initWithData:responseData encoding:NSUTF8StringEncoding];
                           NSLog(@"MSP: Request Succsesful: %@", resp);
                            
                           UnitySendMessage("IOSTwitterManager", "OnUserDataLoaded", [ISNDataConvertor NSStringToChar:resp]);

                            
                        } else {
                            NSLog(@"MSP: No respoce data founded");
                            UnitySendMessage("IOSTwitterManager", "OnUserDataLoadFailed", [ISNDataConvertor NSStringToChar:@""]);
                        }
                    });
                }];
                
                
            } else {
                NSLog(@"MSP: OnUserDataLoadFailed no accounts founded");
                UnitySendMessage("IOSTwitterManager", "OnUserDataLoadFailed", [ISNDataConvertor NSStringToChar:@""]);
            }
            
        } else {
             NSLog(@"MSP: OnUserDataLoadFailed no accses");
            UnitySendMessage("IOSTwitterManager", "OnUserDataLoadFailed", [ISNDataConvertor NSStringToChar:@""]);
        }
    }];

}


-(void) postWithMedia:(NSString *)status media:(NSString *)media {
    NSLog(@"postWithMedia");
    
    NSData *imageData = [[NSData alloc] initWithBase64Encoding:media];
    UIImage *image = [[UIImage alloc] initWithData:imageData];
    
    SLComposeViewController *tweetSheet = [SLComposeViewController composeViewControllerForServiceType:SLServiceTypeTwitter];
    [tweetSheet setInitialText:status];
    [tweetSheet addImage:image];
    
    UIViewController *vc =  UnityGetGLViewController();
    
    [vc presentViewController:tweetSheet animated:YES completion:nil];
    
    tweetSheet.completionHandler = ^(SLComposeViewControllerResult result) {
        switch(result) {
                //  This means the user cancelled without sending the Tweet
            case SLComposeViewControllerResultCancelled:
                NSLog(@"Tweet message was cancelled");
                UnitySendMessage("IOSTwitterManager", "OnPostFailed", [ISNDataConvertor NSStringToChar:@""]);
                [tweetSheet dismissViewControllerAnimated:YES completion:nil];
                break;
                //  This means the user hit 'Send'
            case SLComposeViewControllerResultDone:
                NSLog(@"Done pressed successfully");
                UnitySendMessage("IOSTwitterManager", "OnPostSuccess", [ISNDataConvertor NSStringToChar:@""]);
                [tweetSheet dismissViewControllerAnimated:YES completion:nil];
                break;
        }
    };

}

- (void) post:(NSString *)status {
    
    SLComposeViewController *tweetSheet = [SLComposeViewController composeViewControllerForServiceType:SLServiceTypeTwitter];
    [tweetSheet setInitialText:status];
    
    
    UIViewController *vc =  UnityGetGLViewController();
    
    [vc presentViewController:tweetSheet animated:YES completion:nil];
    
    tweetSheet.completionHandler = ^(SLComposeViewControllerResult result) {
        switch(result) {
                //  This means the user cancelled without sending the Tweet
            case SLComposeViewControllerResultCancelled:
                NSLog(@"Tweet message was cancelled");
                UnitySendMessage("IOSTwitterManager", "OnPostFailed", [ISNDataConvertor NSStringToChar:@""]);
                [tweetSheet dismissViewControllerAnimated:YES completion:nil];
                break;
                //  This means the user hit 'Send'
            case SLComposeViewControllerResultDone:
                NSLog(@"Done pressed successfully");
                UnitySendMessage("IOSTwitterManager", "OnPostSuccess", [ISNDataConvertor NSStringToChar:@""]);
                [tweetSheet dismissViewControllerAnimated:YES completion:nil];
                break;
        }
    };
}


-(BOOL) IsTwitterAvaliable {
    return [SLComposeViewController isAvailableForServiceType:SLServiceTypeTwitter];
}

-(BOOL) IsTwitterAuthed {
    ACAccountStore *account = [[ACAccountStore alloc] init];
    ACAccountType *twitterAccountType = [account accountTypeWithAccountTypeIdentifier:ACAccountTypeIdentifierTwitter];
    
    NSArray *twitterAccounts = [account accountsWithAccountType:twitterAccountType];
    
    if(twitterAccounts.count > 0) {
        return  true;
    } else {
        return  false;
    }
}


extern "C" {
    
    void _twitterInit ()  {
        [[IOSTwitterPlugin sharedInstance] initTwitterPlugin];
    }
    
    
    void _twitterLoadUserData() {
        [[IOSTwitterPlugin sharedInstance] loadUserData];
    }
    
    void _twitterAuthificateUser() {
        [[IOSTwitterPlugin sharedInstance] authificateUser];
    }

    
    void _twitterPost(char* text) {
        NSString *status = [ISNDataConvertor charToNSString:text];
        [[IOSTwitterPlugin sharedInstance] post:status];
    }
    
    void _twitterPostWithMedia(char* text, char* encodedMedia) {
        
        NSString *status = [ISNDataConvertor charToNSString:text];
        NSString *media = [ISNDataConvertor charToNSString:encodedMedia];
       
        [[IOSTwitterPlugin sharedInstance] postWithMedia:status media:media];
    }
    

    
    
}



@end
